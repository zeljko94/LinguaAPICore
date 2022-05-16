using AutoMapper;
using LinguaAPI.Models;
using LinguaAPI.Models.Dapper;
using LinguaAPI.Models.DTOs;
using LinguaAPI.Repositories.Dapper.Interfaces;
using LinguaAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Services.Implementations
{
    public class PredavanjaService : IPredavanjaService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PredavanjaService> _logger;
        private readonly ICalendarEventsRepository _calendarEventsRepository;
        private readonly ITipoviNastaveRepository _tipoviNastaveRepository;
        private readonly IPredavanjaSudioniciRepository _predavanjaSudioniciRepository;
        private readonly IUcioniceRepository _ucioniceRepository;
        private readonly IPredmetiRepository _predmetiRepository;
        private readonly IPredavanjaRepository _predavanjaRepository;
        private readonly IUsersRepository _usersRepository;

        public PredavanjaService(IMapper mapper, ILogger<PredavanjaService> logger, 
            ICalendarEventsRepository calendarEventsRepository,
            ITipoviNastaveRepository tipoviNastaveRepository,
            IPredavanjaSudioniciRepository predavanjaSudioniciRepository,
            IUcioniceRepository ucioniceRepository,
            IPredmetiRepository predmetiRepository,
            IPredavanjaRepository predavanjaRepository, 
            IUsersRepository usersRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _calendarEventsRepository = calendarEventsRepository;
            _tipoviNastaveRepository = tipoviNastaveRepository;
            _predavanjaSudioniciRepository = predavanjaSudioniciRepository;
            _ucioniceRepository = ucioniceRepository;
            _predmetiRepository = predmetiRepository;
            _predavanjaRepository = predavanjaRepository;
            _usersRepository = usersRepository;
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var deleted = await _predavanjaRepository.Delete(id);
            return deleted ? new OkResponse<bool>("Uspješno obrisano!", true) : new ErrorResponse<bool>("Greška prilkom brisanja!", false);
        }

        public async Task<ApiResponse<List<PredavanjeDTO>>> GetAll()
        {
            try
            {
                var entities = await _predavanjaRepository.GetAll();
                var dtos = _mapper.Map<List<PredavanjeDTO>>(entities);
                for(int i=0; i<dtos.Count;i++)
                {
                    var toUpdate = dtos.ElementAt(i);
                    dtos[i] = await IncludeRelations(toUpdate);
                    i++;
                }
                return new OkResponse<List<PredavanjeDTO>>("Uspješan dohvat!", dtos);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorResponse<List<PredavanjeDTO>>("Greška prilikom dohvata!", null);
            }
        }

        public async Task<ApiResponse<PredavanjeDTO>> GetById(int id)
        {
            try
            {
                var entity = await _predavanjaRepository.GetById(id);
                var dto = _mapper.Map<PredavanjeDTO>(entity);
                dto = await IncludeRelations(dto);
                return new OkResponse<PredavanjeDTO>("Uspješan dohvat!", dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorResponse<PredavanjeDTO>("Greška prilikom dohvata!", null);
            }
        }

        public async Task<ApiResponse<int>> Insert(PredavanjeDTO dto)
        {
            try
            {
                var entity = _mapper.Map<Predavanje>(dto);
                var inserted = await _predavanjaRepository.Insert(entity);
                return inserted > 0 ? new OkResponse<int>("Uspješan unos!", inserted) : new ErrorResponse<int>("Greška prilikom unosa!", 0);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorResponse<int>("Greška prilikom unosa!", 0);
            }
        }

        public async Task<ApiResponse<bool>> Update(PredavanjeDTO dto)
        {
            try
            {
                var entity = _mapper.Map<Predavanje>(dto);
                var updated = await _predavanjaRepository.Update(entity);
                var sudionici = await _predavanjaSudioniciRepository.GetForPredavanje(entity.Id);
                var deletedSudionici = await _predavanjaSudioniciRepository.DeleteRange(sudionici);
                if (!deletedSudionici)
                    return new ErrorResponse<bool>("Greška prilikom ažuriranja!", false);
                var insertedSudionci = await _predavanjaSudioniciRepository.InsertRange(_mapper.Map<List<PredavanjeSudionik>>(dto.PredavanjeSudionici));
                if(!insertedSudionci)
                    return new ErrorResponse<bool>("Greška prilikom ažuriranja!", false);

                return new OkResponse<bool>("Uspješno ažurirano!", true);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorResponse<bool>("Greška prilikom ažuriranja!", false);
            }
        }


        private async Task<PredavanjeDTO> IncludeRelations(PredavanjeDTO dto, List<string> relations = null)
        {
            if(relations == null || relations.Contains("Profesor"))
                dto.Profesor = await _usersRepository.GetById(dto.ProfesorId.Value);
            if (relations == null || relations.Contains("Predmet"))
                dto.Predmet = await _predmetiRepository.GetById(dto.PredmetId.Value);
            if (relations == null || relations.Contains("Ucionica"))
                dto.Ucionica = await _ucioniceRepository.GetById(dto.UcionicaId.Value);
            if (relations == null || relations.Contains("TipNastave"))
                dto.TipNastave = await _tipoviNastaveRepository.GetById(dto.TipNastaveId.Value);
            if (relations == null || relations.Contains("CalendarEvent"))
                dto.CalendarEvents = _mapper.Map<List<CalendarEventDTO>>(await _calendarEventsRepository.GetForPredavanje(dto.Id));
            if (relations == null || relations.Contains("PredavanjeSudionici"))
            {
                dto.PredavanjeSudionici = new List<PredavanjeSudionikDTO>();
                var sudionici = await _predavanjaSudioniciRepository.GetForPredavanje(dto.Id);
                foreach (var sudionik in sudionici)
                {
                    var sudionikDTO = _mapper.Map<PredavanjeSudionikDTO>(sudionik);
                    sudionikDTO.User = await _usersRepository.GetById(sudionikDTO.UserId.Value);
                    dto.PredavanjeSudionici.Add(sudionikDTO);
                }
            }
            return dto;
        }
    }
}

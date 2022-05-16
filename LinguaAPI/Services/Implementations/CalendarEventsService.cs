using AutoMapper;
using LinguaAPI.Models;
using LinguaAPI.Models.Dapper;
using LinguaAPI.Models.DTOs;
using LinguaAPI.Repositories.Dapper.Interfaces;
using LinguaAPI.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinguaAPI.Services.Implementations
{
    public class CalendarEventsService : ICalendarEventsService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<CalendarEventsService> _logger;
        private readonly ICalendarEventsRepository _calendarEventsRepository;
        private readonly IPredavanjaRepository _predavanjaRepository;

        public CalendarEventsService(IMapper mapper, ILogger<CalendarEventsService> logger,
            ICalendarEventsRepository calendarEventsRepository, 
            IPredavanjaRepository predavanjaRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _calendarEventsRepository = calendarEventsRepository;
            _predavanjaRepository = predavanjaRepository;
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var deleted = await _calendarEventsRepository.Delete(id);
            return deleted ? new OkResponse<bool>("Uspješno obrisano!", true) : new ErrorResponse<bool>("Greška prilikom brisanja!", false);
        }

        public async Task<ApiResponse<List<CalendarEventDTO>>> GetAll()
        {
            try
            {
                var entities = await _calendarEventsRepository.GetAll();
                var dtos = _mapper.Map<List<CalendarEventDTO>>(entities);
                for (int i = 0; i < dtos.Count; i++)
                {
                    dtos[i].Predavanje = _mapper.Map<PredavanjeDTO>(await _predavanjaRepository.GetById(dtos[i].PredavanjeId));
                }
                return new OkResponse<List<CalendarEventDTO>>("Uspješan dohvat!", dtos);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorResponse<List<CalendarEventDTO>>("Greška prilikom dohvata!", null);
            }
        }

        public async Task<ApiResponse<CalendarEventDTO>> GetById(int id)
        {
            try
            {
                var entity = await _calendarEventsRepository.GetById(id);
                var dto = _mapper.Map<CalendarEventDTO>(entity);
                dto.Predavanje = _mapper.Map<PredavanjeDTO>(await _predavanjaRepository.GetById(dto.PredavanjeId));
                return new OkResponse<CalendarEventDTO>("Uspješan dohvat!", dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorResponse<CalendarEventDTO>("Greška prilikom dohvata!", null);
            }
        }

        public async Task<ApiResponse<int>> Insert(CalendarEventDTO dto)
        {
            try
            {
                var entity = _mapper.Map<CalendarEvent>(dto);
                var inserted = await _calendarEventsRepository.Insert(entity);
                return inserted > 0 ? new OkResponse<int>("Uspješan unos!", inserted) : new ErrorResponse<int>("Greška prilikom unosa!", 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorResponse<int>("Greška prilikom unosa!", 0);
            }
        }

        public async Task<ApiResponse<bool>> Update(CalendarEventDTO dto)
        {
            try
            {
                var entity = _mapper.Map<CalendarEvent>(dto);
                var updated = await _calendarEventsRepository.Update(entity);
                return updated ? new OkResponse<bool>("Uspješno ažurirano!", true) : new ErrorResponse<bool>("Greška prilikom ažuriranja!", false);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorResponse<bool>("Greška prilikom ažuriranja!", false);
            }
        }
    }
}

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
    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<UsersService> _logger;

        public UsersService(IMapper mapper, IUsersRepository usersRepository, ILogger<UsersService> logger)
        {
            _mapper = mapper;
            _usersRepository = usersRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> Delete(int id)
        {
            var deleted = await _usersRepository.Delete(id);
            return deleted ? new OkResponse<bool>("Uspješno obrisano", deleted) : new ErrorResponse<bool>("Greška prilikom brisanja!", false);
        }

        public async Task<ApiResponse<List<UserDTO>>> GetAll()
        {
            try
            {
                var users = await _usersRepository.GetAll();
                var dtos = _mapper.Map<List<UserDTO>>(users);
                return new OkResponse<List<UserDTO>>("Uspješan dohvat!", dtos);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorResponse<List<UserDTO>>("Greška prilikom dohvata!", null);
            }
        }

        public async Task<ApiResponse<UserDTO>> GetById(int id)
        {
            try
            {
                var entity = await _usersRepository.GetById(id);
                if (entity == null)
                    return new ErrorResponse<UserDTO>("Korisnik nije pronađen!", null);

                var dto = _mapper.Map<UserDTO>(entity);
                return new OkResponse<UserDTO>("Uspješan dohvat", dto);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorResponse<UserDTO>("Greška prilikom dohvata!", null);
            }
        }

        public async Task<ApiResponse<int>> Insert(UserDTO dto)
        {
            try
            {
                var entity = _mapper.Map<User>(dto);
                var inserted = await _usersRepository.Insert(entity);
                return inserted > 0 ? new OkResponse<int>("Uspješno uneseno!", inserted) : new ErrorResponse<int>("Greška prilikom unosa!", 0);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new ErrorResponse<int>("Greška prilikom unosa!", 0);
            }
        }

        public async Task<ApiResponse<bool>> Update(UserDTO dto)
        {
            try
            {
                var entity = _mapper.Map<User>(dto);
                var updated = await _usersRepository.Update(entity);
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

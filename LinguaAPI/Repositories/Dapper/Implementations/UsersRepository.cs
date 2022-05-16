using LinguaAPI.Models.Dapper;
using LinguaAPI.Repositories.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Implementations
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IBaseRepository<User> _baseRepository;

        public UsersRepository(IBaseRepository<User> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<bool> Delete(int id)
        {
            return await _baseRepository.Delete(id);
        }

        public async Task<List<User>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<User> GetById(int id)
        {
            return await _baseRepository.GetById(id);
        }

        public async Task<int> Insert(User entity)
        {
            return await _baseRepository.Insert(entity);
        }

        public async Task<bool> Update(User entity)
        {
            return await _baseRepository.Update(entity);
        }
    }
}

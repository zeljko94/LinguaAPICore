using LinguaAPI.Models.Dapper;
using LinguaAPI.Repositories.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Implementations
{
    public class UcioniceRepository : IUcioniceRepository
    {
        private readonly IBaseRepository<Ucionica> _baseRepository;

        public UcioniceRepository(IBaseRepository<Ucionica> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<bool> Delete(int id)
        {
            return await _baseRepository.Delete(id);
        }

        public async Task<List<Ucionica>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<Ucionica> GetById(int id)
        {
            return await _baseRepository.GetById(id);
        }

        public async Task<int> Insert(Ucionica entity)
        {
            return await _baseRepository.Insert(entity);
        }

        public async Task<bool> Update(Ucionica entity)
        {
            return await _baseRepository.Update(entity);
        }
    }
}

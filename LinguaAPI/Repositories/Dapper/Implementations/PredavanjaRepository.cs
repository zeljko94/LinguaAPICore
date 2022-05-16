using LinguaAPI.Models.Dapper;
using LinguaAPI.Repositories.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Implementations
{
    public class PredavanjaRepository : IPredavanjaRepository
    {
        private readonly IBaseRepository<Predavanje> _baseRepository;

        public PredavanjaRepository(IBaseRepository<Predavanje> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<bool> Delete(int id)
        {
            return await _baseRepository.Delete(id);
        }

        public async Task<List<Predavanje>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<Predavanje> GetById(int id)
        {
            return await _baseRepository.GetById(id);
        }

        public async Task<int> Insert(Predavanje entity)
        {
            return await _baseRepository.Insert(entity);
        }

        public async Task<bool> Update(Predavanje entity)
        {
            return await _baseRepository.Update(entity);
        }
    }
}

using LinguaAPI.Models.Dapper;
using LinguaAPI.Repositories.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Implementations
{
    public class PredmetiRepository : IPredmetiRepository
    {
        private readonly IBaseRepository<Predmet> _baseRepository;

        public PredmetiRepository(IBaseRepository<Predmet> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<bool> Delete(int id)
        {
            return await _baseRepository.Delete(id);
        }

        public async Task<List<Predmet>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<Predmet> GetById(int id)
        {
            return await _baseRepository.GetById(id);
        }

        public async Task<int> Insert(Predmet entity)
        {
            return await _baseRepository.Insert(entity);
        }

        public async Task<bool> Update(Predmet entity)
        {
            return await _baseRepository.Update(entity);
        }
    }
}

using LinguaAPI.Models.Dapper;
using LinguaAPI.Repositories.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Implementations
{
    public class DokumentiRepository : IDokumentiRepository
    {
        private readonly IBaseRepository<Dokument> _baseRepository;

        public DokumentiRepository(IBaseRepository<Dokument> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<bool> Delete(int id)
        {
            return await _baseRepository.Delete(id);
        }

        public async Task<List<Dokument>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<Dokument> GetById(int id)
        {
            return await _baseRepository.GetById(id);
        }

        public async Task<int> Insert(Dokument entity)
        {
            return await _baseRepository.Insert(entity);
        }

        public async Task<bool> Update(Dokument entity)
        {
            return await _baseRepository.Update(entity);
        }
    }
}

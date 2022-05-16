using LinguaAPI.Models.Dapper;
using LinguaAPI.Repositories.Dapper.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Implementations
{
    public class TipoviNastaveRepository : ITipoviNastaveRepository
    {
        private readonly IBaseRepository<TipNastave> _baseRepository;

        public TipoviNastaveRepository(IBaseRepository<TipNastave> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<bool> Delete(int id)
        {
            return await _baseRepository.Delete(id);
        }

        public async Task<List<TipNastave>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<TipNastave> GetById(int id)
        {
            return await _baseRepository.GetById(id);
        }

        public async Task<int> Insert(TipNastave entity)
        {
            return await _baseRepository.Insert(entity);
        }

        public async Task<bool> Update(TipNastave entity)
        {
            return await _baseRepository.Update(entity);
        }
    }
}

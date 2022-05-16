using LinguaAPI.Models.Dapper;
using LinguaAPI.Repositories.Dapper.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Implementations
{
    public class PredavanjaSudioniciRepository : IPredavanjaSudioniciRepository
    {
        private readonly IBaseRepository<PredavanjeSudionik> _baseRepository;
        private readonly IBaseRepository<User> _usersBaseRepository;

        public PredavanjaSudioniciRepository(
            IBaseRepository<PredavanjeSudionik> baseRepository, 
            IBaseRepository<User> usersBaseRepository
            )
        {
            _baseRepository = baseRepository;
            _usersBaseRepository = usersBaseRepository;
        }

        public async Task<bool> Delete(int id)
        {
            return await _baseRepository.Delete(id);
        }

        public async Task<List<PredavanjeSudionik>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<PredavanjeSudionik> GetById(int id)
        {
            return await _baseRepository.GetById(id);
        }

        public async Task<int> Insert(PredavanjeSudionik entity)
        {
            return await _baseRepository.Insert(entity);
        }

        public async Task<bool> Update(PredavanjeSudionik entity)
        {
            return await _baseRepository.Update(entity);
        }

        public async Task<List<PredavanjeSudionik>> GetForPredavanje(int predavanjeId)
        {
            var sudionici = await _baseRepository.Query("SELECT * FROM [LinguaDB].[dbo].[PredavanjeSudionik] WHERE [PredavanjeId]=@id", new List<QueryParameter> { new() { Name = "id", Value = predavanjeId } });
            return sudionici;
        }

        public async Task<bool> InsertRange(List<PredavanjeSudionik> sudionici)
        {
            return await _baseRepository.InsertRange(sudionici);
        }

        public async Task<bool> DeleteRange(List<PredavanjeSudionik> sudionici)
        {
            return await _baseRepository.DeleteRange(sudionici);
        }
    }
}

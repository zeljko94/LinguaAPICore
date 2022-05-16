using LinguaAPI.Models.Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Interfaces
{
    public interface IPredavanjaSudioniciRepository
    {
        Task<List<PredavanjeSudionik>> GetAll();
        Task<PredavanjeSudionik> GetById(int id);
        Task<int> Insert(PredavanjeSudionik entity);
        Task<bool> Update(PredavanjeSudionik entity);
        Task<bool> Delete(int id);

        Task<bool> InsertRange(List<PredavanjeSudionik> sudionici);
        Task<bool> DeleteRange(List<PredavanjeSudionik> sudionici);

        Task<List<PredavanjeSudionik>> GetForPredavanje(int predavanjeId);
    }
}

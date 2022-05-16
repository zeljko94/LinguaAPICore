using LinguaAPI.Models.Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Interfaces
{
    public interface IDokumentiRepository
    {
        Task<List<Dokument>> GetAll();
        Task<Dokument> GetById(int id);
        Task<int> Insert(Dokument entity);
        Task<bool> Update(Dokument entity);
        Task<bool> Delete(int id);
    }
}

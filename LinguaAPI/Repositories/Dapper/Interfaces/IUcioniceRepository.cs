using LinguaAPI.Models.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Interfaces
{
    public interface IUcioniceRepository
    {
        Task<List<Ucionica>> GetAll();
        Task<Ucionica> GetById(int id);
        Task<int> Insert(Ucionica entity);
        Task<bool> Update(Ucionica entity);
        Task<bool> Delete(int id);
    }
}

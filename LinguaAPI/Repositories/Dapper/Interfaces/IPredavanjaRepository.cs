using LinguaAPI.Models.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Interfaces
{
    public interface IPredavanjaRepository
    {
        Task<List<Predavanje>> GetAll();
        Task<Predavanje> GetById(int id);
        Task<int> Insert(Predavanje entity);
        Task<bool> Update(Predavanje entity);
        Task<bool> Delete(int id);
    }
}

using LinguaAPI.Models.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Interfaces
{
    public interface IPredmetiRepository
    {
        Task<List<Predmet>> GetAll();
        Task<Predmet> GetById(int id);
        Task<int> Insert(Predmet entity);
        Task<bool> Update(Predmet entity);
        Task<bool> Delete(int id);
    }
}

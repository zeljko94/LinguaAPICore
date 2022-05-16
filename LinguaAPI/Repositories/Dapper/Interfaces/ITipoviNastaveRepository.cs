using LinguaAPI.Models.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Interfaces
{
    public interface ITipoviNastaveRepository
    {
        Task<List<TipNastave>> GetAll();
        Task<TipNastave> GetById(int id);
        Task<int> Insert(TipNastave entity);
        Task<bool> Update(TipNastave entity);
        Task<bool> Delete(int id);
    }
}

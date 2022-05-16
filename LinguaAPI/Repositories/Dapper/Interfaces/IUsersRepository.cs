using LinguaAPI.Models.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Interfaces
{
    public interface IUsersRepository
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<int> Insert(User entity);
        Task<bool> Update(User entity);
        Task<bool> Delete(int id);
    }
}

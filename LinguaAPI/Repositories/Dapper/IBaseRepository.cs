using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<int> Insert(T entity);
        Task<bool> Update(T entity);
        Task<bool> InsertRange(List<T> entities);
        Task<bool> DeleteRange(List<T> entities);
        Task<bool> Delete(int id);
        Task<T> ParseFromReader(SqlDataReader reader);
        Task<List<T>> ParseListFromReader(SqlDataReader reader);

        DataTable ToDataTable(IList<T> data);


        Task<List<T>> Query(string query, List<QueryParameter> parameters);
        Task<T> QuerySingle(string query, List<QueryParameter> parameters);
    }
}

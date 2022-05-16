using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace LinguaAPI.Repositories.Dapper
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<BaseRepository<T>> _logger;
        private readonly string _tableName;

        public BaseRepository(IConfiguration configuration, ILogger<BaseRepository<T>> logger)
        {
            _configuration = configuration;
            _logger = logger;
            _tableName = typeof(T).Name;
        }

        public async Task<List<T>> GetAll()
        {
            try
            {
                using (var con = new SqlConnection())
                {
                    con.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
                    await con.OpenAsync();

                    SqlCommand command = new SqlCommand($"SELECT * FROM [{_tableName}]", con);
                    command.CommandTimeout = 0;

                    try
                    {
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        return await ParseListFromReader(reader);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        return null;
                    }
                    finally
                    {
                        await command.DisposeAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                using (var con = new SqlConnection())
                {
                    con.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
                    await con.OpenAsync();

                    SqlCommand command = new SqlCommand($"SELECT * FROM [{_tableName}] WHERE [Id]=@id", con);
                    command.Parameters.AddWithValue("id", id);
                    command.CommandTimeout = 0;

                    try
                    {
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        return await ParseFromReader(reader);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        return null;
                    }
                    finally
                    {
                        await command.DisposeAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<int> Insert(T entity)
        {
            try
            {
                entity.DatumKreiranja = DateTime.Now;
                using (var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await con.OpenAsync();

                    string query = $"INSERT INTO [LinguaDB].[dbo].[{_tableName}]";
                    string cols = "(";
                    string vals = " VALUES(";
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    foreach (PropertyInfo prop in entity.GetType().GetProperties())
                    {
                        var hasNotMappedAttribute = prop.GetCustomAttributes(true).Any(x => x.GetType() == typeof(NotMappedAttribute));
                        if (prop.Name != "Id" && prop.Name != "ID" && prop.Name != "id" && prop.GetType().IsClass && !hasNotMappedAttribute)
                        {
                            //if (prop.PropertyType == typeof(DateTime?) && prop.Name == "DatumKreiranja")
                            //{
                            //    prop.SetValue(entity, DateTime.Now);
                            //}
                            if (prop == entity.GetType().GetProperties().Last())
                            {
                                cols += "[" + prop.Name + "]";
                                vals += "@" + prop.Name;
                            }
                            else
                            {
                                cols += "[" + prop.Name + "], ";
                                vals += "@" + prop.Name + ", ";
                            }
                            var propValue = prop.GetValue(entity, null);
                            if (propValue == null)
                            {
                                parameters.Add(new SqlParameter(prop.Name, DBNull.Value));
                            }
                            else
                            {
                                parameters.Add(new SqlParameter(prop.Name, propValue));
                            }
                        }
                    }
                    cols += ")";
                    vals += ");";
                    query += cols + vals + "SELECT cast(SCOPE_IDENTITY() as int);";
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.AddRange(parameters.ToArray());
                        command.CommandTimeout = 0;

                        int insertedId = 0;
                        try
                        {
                            insertedId = (int)await command.ExecuteScalarAsync();
                            return insertedId;                        
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, ex.Message);
                            return 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return 0;
            }
        }

        public async Task<bool> Update(T entity)
        {
            T obj = await GetById(entity.Id);
            if (obj == null)
                return false;
            CopyClass(entity, obj);
            obj.DatumAzuriranja = DateTime.Now;


            try
            {
                using (var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await con.OpenAsync();

                    string query = $"UPDATE [LinguaDB].[dbo].[{_tableName}]";
                    string update = " SET ";
                    List<SqlParameter> parameters = new List<SqlParameter>();
                    foreach (PropertyInfo prop in obj.GetType().GetProperties())
                    {
                        var hasNotMappedAttribute = prop.GetCustomAttributes(true).Any(x => x.GetType() == typeof(NotMappedAttribute));
                        if (prop.Name != "Id" && prop.Name != "ID" && prop.Name != "id" && prop.GetType().IsClass && !hasNotMappedAttribute)
                        {
                            if (prop == obj.GetType().GetProperties().Last())
                            {
                                update += "[" + prop.Name + "]=@" + prop.Name;
                            }
                            else
                            {
                                update += "[" + prop.Name + "]=@" + prop.Name + ", ";
                            }
                        }
                        parameters.Add(new SqlParameter(prop.Name, prop.GetValue(obj)));
                    }
                    update += " WHERE [Id]=@Id;";
                    query += update;
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        foreach(var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.ParameterName, param.Value != null ? param.Value : DBNull.Value);
                        }
                        //command.Parameters.AddRange(parameters.ToArray());
                        command.CommandTimeout = 0;

                        int updatedId = 0;
                        try
                        {
                            updatedId = (int)await command.ExecuteNonQueryAsync();
                            return updatedId > 0;
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, ex.Message);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                using (var con = new SqlConnection())
                {
                    con.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
                    await con.OpenAsync();

                    SqlCommand command = new SqlCommand($"DELETE FROM [{_tableName}] WHERE [Id]=@id", con);
                    command.Parameters.AddWithValue("id", id);
                    command.CommandTimeout = 0;

                    try
                    {
                        int deletedRows = await command.ExecuteNonQueryAsync();
                        return deletedRows > 0;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        return false;
                    }
                    finally
                    {
                        await command.DisposeAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        private void CopyClass(T copyFrom, T copyTo)
        {
            if (copyFrom == null || copyTo == null)
                throw new Exception("Must not specify null parameters");

            var properties = copyFrom.GetType().GetProperties();

            foreach (var p in properties.Where(prop => prop.CanRead && prop.CanWrite))
            {
                object copyValue = p.GetValue(copyFrom);
                if (copyValue != null)
                {
                    p.SetValue(copyTo, copyValue);
                }
            }
        }

        public async Task<T> ParseFromReader(SqlDataReader reader)
        {
            return (await ParseListFromReader(reader)).FirstOrDefault();
        }

        public async Task<List<T>> ParseListFromReader(SqlDataReader reader)
        {
            var results = new List<T>();

            try
            {
                while (reader.Read())
                {
                    var item = Activator.CreateInstance<T>();
                    foreach (var property in typeof(T).GetProperties())
                    {
                        var hasNotMappedAttribute = property.GetCustomAttributes(true).Any(x => x.GetType() == typeof(NotMappedAttribute));
                        if (!hasNotMappedAttribute && !reader.IsDBNull(reader.GetOrdinal(property.Name)))
                        {
                            Type convertTo = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                            if(property.PropertyType.IsEnum)
                            {
                                var whatever = Enum.ToObject(property.PropertyType, reader[property.Name]);
                                property.SetValue(item, whatever, null);
                            }
                            else
                            {
                                property.SetValue(item, Convert.ChangeType(reader[property.Name], convertTo), null);
                            }
                        }
                    }
                    results.Add(item);
                }
                return await Task.FromResult(results);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<T> QuerySingle(string query, List<QueryParameter> parameters)
        {
            var results = await Query(query, parameters);
            return results.FirstOrDefault();
        }

        public async Task<List<T>> Query(string query, List<QueryParameter> parameters)
        {
            try
            {
                using (var con = new SqlConnection())
                {
                    con.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
                    await con.OpenAsync();

                    SqlCommand command = new SqlCommand(query, con);
                    foreach(var param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Name, param.Value);
                    }
                    command.CommandTimeout = 0;

                    try
                    {
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        return await ParseListFromReader(reader);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        return null;
                    }
                    finally
                    {
                        await command.DisposeAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> InsertRange(List<T> entities)
        {
            try
            {
                using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    using (var transaction = conn.BeginTransaction())
                    using (var copy = new SqlBulkCopy(conn, SqlBulkCopyOptions.KeepIdentity, transaction))
                    {
                        copy.DestinationTableName = "dbo." + _tableName;

                        var props = typeof(T).GetProperties();
                        foreach(var prop in props)
                        {
                            copy.ColumnMappings.Add(prop.Name, prop.Name);
                        }

                        //copy.ColumnMappings.Add(nameof(KadrovskaBrojRadnihDana.DohvatId), "DohvatId");

                        try
                        {
                            copy.WriteToServer(ToDataTable(entities));
                            await transaction.CommitAsync();
                            return await Task.FromResult(true);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, ex.Message);
                            await transaction.RollbackAsync();
                            return await Task.FromResult(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteRange(List<T> entities)
        {
            if (entities.Count < 1)
                return true;

            try
            {
                using (var con = new SqlConnection())
                {
                    con.ConnectionString = _configuration.GetConnectionString("DefaultConnection");
                    await con.OpenAsync();

                    var query = $"DELETE FROM [{_tableName}] WHERE [Id] IN(";
                    var i = 0;
                    var last = entities.Last();
                    foreach (var entity in entities)
                    {
                        if(entity.Equals(last))
                        {
                            query += "@id" + i;
                        }
                        else
                        {
                            query += "@id" + i + ", ";
                        }
                        i++;
                    }
                    query += ")";
                    SqlCommand command = new SqlCommand(query, con);
                    i = 0;
                    foreach (var entity in entities)
                    {
                        command.Parameters.AddWithValue("id" + i.ToString(), entity.Id);
                        i++;
                    }
                    command.CommandTimeout = 0;

                    try
                    {
                        int deletedRows = await command.ExecuteNonQueryAsync();
                        return deletedRows > 0;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        return false;
                    }
                    finally
                    {
                        await command.DisposeAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public DataTable ToDataTable(IList<T> data)
        {
            DataTable table = new DataTable();

            //special handling for value types and string
            if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {

                DataColumn dc = new DataColumn("Value", typeof(T));
                table.Columns.Add(dc);
                foreach (T item in data)
                {
                    DataRow dr = table.NewRow();
                    dr[0] = item;
                    table.Rows.Add(dr);
                }
            }
            else
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                foreach (PropertyDescriptor prop in properties)
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        try
                        {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                        catch (Exception ex)
                        {
                            row[prop.Name] = DBNull.Value;
                        }
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }
    }
}

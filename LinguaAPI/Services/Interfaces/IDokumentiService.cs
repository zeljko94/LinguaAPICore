using LinguaAPI.Models;
using LinguaAPI.Models.Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinguaAPI.Services.Interfaces
{
    public interface IDokumentiService
    {
        Task<ApiResponse<List<Dokument>>> GetAll();
        Task<ApiResponse<Dokument>> GetById(int id);
        Task<ApiResponse<int>> Insert(Dokument entity);
        Task<ApiResponse<bool>> Update(Dokument entity);
        Task<ApiResponse<bool>> Delete(int id);
    }
}

using LinguaAPI.Models;
using LinguaAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinguaAPI.Services.Interfaces
{
    public interface IPredavanjaService
    {
        Task<ApiResponse<List<PredavanjeDTO>>> GetAll();
        Task<ApiResponse<PredavanjeDTO>> GetById(int id);
        Task<ApiResponse<int>> Insert(PredavanjeDTO entity);
        Task<ApiResponse<bool>> Update(PredavanjeDTO entity);
        Task<ApiResponse<bool>> Delete(int id);
    }
}

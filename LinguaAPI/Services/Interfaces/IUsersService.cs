using LinguaAPI.Models;
using LinguaAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinguaAPI.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ApiResponse<List<UserDTO>>> GetAll();
        Task<ApiResponse<UserDTO>> GetById(int id);
        Task<ApiResponse<int>> Insert(UserDTO entity);
        Task<ApiResponse<bool>> Update(UserDTO entity);
        Task<ApiResponse<bool>> Delete(int id);
    }
}

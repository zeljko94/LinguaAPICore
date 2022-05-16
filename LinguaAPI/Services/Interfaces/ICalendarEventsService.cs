using LinguaAPI.Models;
using LinguaAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinguaAPI.Services.Interfaces
{
    public interface ICalendarEventsService
    {
        Task<ApiResponse<List<CalendarEventDTO>>> GetAll();
        Task<ApiResponse<CalendarEventDTO>> GetById(int id);
        Task<ApiResponse<int>> Insert(CalendarEventDTO entity);
        Task<ApiResponse<bool>> Update(CalendarEventDTO entity);
        Task<ApiResponse<bool>> Delete(int id);
    }
}

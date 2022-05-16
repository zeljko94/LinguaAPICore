using LinguaAPI.Models.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Interfaces
{
    public interface ICalendarEventsRepository
    {
        Task<List<CalendarEvent>> GetAll();
        Task<CalendarEvent> GetById(int id);
        Task<int> Insert(CalendarEvent entity);
        Task<bool> Update(CalendarEvent entity);
        Task<bool> Delete(int id);

        Task<List<CalendarEvent>> GetForPredavanje(int predavanjeId);
    }
}

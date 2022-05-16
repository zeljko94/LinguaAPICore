using LinguaAPI.Models.Dapper;
using LinguaAPI.Repositories.Dapper.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LinguaAPI.Repositories.Dapper.Implementations
{
    public class CalendarEventsRepository : ICalendarEventsRepository
    {
        private readonly IBaseRepository<CalendarEvent> _baseRepository;

        public CalendarEventsRepository(IBaseRepository<CalendarEvent> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<bool> Delete(int id)
        {
            return await _baseRepository.Delete(id);
        }

        public async Task<List<CalendarEvent>> GetAll()
        {
            return await _baseRepository.GetAll();
        }

        public async Task<CalendarEvent> GetById(int id)
        {
            return await _baseRepository.GetById(id);
        }

        public async Task<int> Insert(CalendarEvent entity)
        {
            return await _baseRepository.Insert(entity);
        }

        public async Task<bool> Update(CalendarEvent entity)
        {
            return await _baseRepository.Update(entity);
        }
        public async Task<List<CalendarEvent>> GetForPredavanje(int predavanjeId)
        {
            var events = await _baseRepository.Query("SELECT * FROM [LinguaDB].[dbo].[CalendarEvent] WHERE [PredavanjeId]=@id", new List<QueryParameter> { new() { Name = "id", Value = predavanjeId } });
            return events;
        }
    }
}

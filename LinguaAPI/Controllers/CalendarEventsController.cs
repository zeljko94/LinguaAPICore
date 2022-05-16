using LinguaAPI.Models.DTOs;
using LinguaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinguaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarEventsController : ControllerBase
    {
        private readonly ICalendarEventsService _calendarEventsService;

        public CalendarEventsController(ICalendarEventsService calendarEventsService)
        {
            _calendarEventsService = calendarEventsService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _calendarEventsService.GetAll());
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _calendarEventsService.GetById(id));
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] CalendarEventDTO calendarEventDTO)
        {
            return Ok(await _calendarEventsService.Insert(calendarEventDTO));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] CalendarEventDTO calendarEventDTO)
        {
            return Ok(await _calendarEventsService.Update(calendarEventDTO));
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _calendarEventsService.Delete(id));
        }
    }
}

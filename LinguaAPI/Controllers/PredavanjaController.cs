using LinguaAPI.Models.DTOs;
using LinguaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LinguaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredavanjaController : ControllerBase
    {
        private readonly IPredavanjaService _predavanjaService;

        public PredavanjaController(IPredavanjaService predavanjaService)
        {
            _predavanjaService = predavanjaService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _predavanjaService.GetAll());
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _predavanjaService.GetById(id));
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] PredavanjeDTO predavanje)
        {
            return Ok(await _predavanjaService.Insert(predavanje));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] PredavanjeDTO predavanje)
        {
            return Ok(await _predavanjaService.Update(predavanje));
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _predavanjaService.Delete(id));
        }
    }
}

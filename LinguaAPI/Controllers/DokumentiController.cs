using LinguaAPI.Models.Dapper;
using LinguaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinguaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DokumentiController : ControllerBase
    {
        private readonly IDokumentiService _dokumentiService;

        public DokumentiController(IDokumentiService dokumentiService)
        {
            _dokumentiService = dokumentiService;
        }


        [HttpPost("Test1")]
        public async Task<IActionResult> Test1([FromBody] Dokument dokument)
        {
            var file1Path = @"C:\Users\zeljk\Desktop\Models\testdocs\deploy.txt";
            var file2Path = @"C:\Users\zeljk\Desktop\Models\testdocs\Dokument interoperabilnosti_JOPPD_v1-1(1).docx";
            var file3Path = @"C:\Users\zeljk\Desktop\Models\testdocs\slika2.png";
            var file4Path = @"C:\Users\zeljk\Desktop\Models\testdocs\error-logs";

            dokument.Bytes = await System.IO.File.ReadAllBytesAsync(file1Path);
            var inserted = await _dokumentiService.Insert(dokument);

            return Ok(inserted);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _dokumentiService.GetAll());
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _dokumentiService.GetById(id));
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] Dokument dokument)
        {
            return Ok(await _dokumentiService.Insert(dokument));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Dokument dokument)
        {
            return Ok(await _dokumentiService.Update(dokument));
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _dokumentiService.Delete(id));
        }
    }
}

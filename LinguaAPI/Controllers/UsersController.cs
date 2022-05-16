using LinguaAPI.Models.Dapper;
using LinguaAPI.Models.DTOs;
using LinguaAPI.Repositories.Dapper;
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
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _usersService.GetAll());
        }

        [HttpGet("GetById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _usersService.GetById(id));
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] UserDTO user)
        {
            return Ok(await _usersService.Insert(user));
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UserDTO user)
        {
            return Ok(await _usersService.Update(user));
        }

        [HttpDelete("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _usersService.Delete(id));
        }
    }
}

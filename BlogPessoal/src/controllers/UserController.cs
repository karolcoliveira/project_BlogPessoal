using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using BlogPessoal.src.utilities;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Atributes

        private readonly IUser _repository;
        private readonly IAuthentication _services;

        #endregion Atributes

        #region Constructors

        public UserController(IUser repository, IAuthentication service)

        { _repository = repository;
          _services = service;
        }

        #endregion Constructors

        #region Methods

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddUser([FromBody] AddUserDTO user)
        {
            if(!ModelState.IsValid) return BadRequest();

            try
            {
                await _services.AddUserWithoutDuplicateAsync(user);
                return Created($"api/Users/email/{user.Email}", user);
            }
            catch (Exception ex)
            { 
                return Unauthorized(ex.Message); 
            }
        }


        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest(user);

            user.Password = _services.CodifyPassword(user.Password);

            await  _repository.UpdateUserAsync(user);
            return Ok(user);
        }


        [HttpDelete("delete/{idUser}")]
        [Authorize(Roles ="ADMINISTRATOR")]
        public async Task<ActionResult> DeleteUser([FromRoute] int idUser)
        {
            await  _repository.DeleteUserAsync(idUser);
            return NoContent();
        }


        [HttpGet("id/{user}")]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByIdAsync([FromRoute] int iduser)
        {
            var user = await _repository.GetUserByIdAsync(iduser);

            if (user == null) return NotFound();
            return Ok(user);
        }


        [HttpGet]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByNameAsync([FromQuery] string nameUser)
        {
            var users = await _repository.GetUserByNameAsync(nameUser);
            if (users.Count < 1) return NoContent();
            return Ok(users);
        }


        [HttpGet("email/{emailUser}")]
        [Authorize(Roles = "NORMAL, ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string emailUser)
        {
            var user = await _repository.GetUserByEmailAsync(emailUser);

            if (user == null) return NotFound();

            return Ok(user);
        }
        #endregion
    }
}

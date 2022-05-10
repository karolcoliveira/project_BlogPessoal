using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

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

        public UserController(IUser repository, IAuthentication services)

        { _repository = repository;
          _services = services;
        }

        #endregion Constructors

        #region Methods

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddUser([FromBody] AddUserDTO user)
        {
            if(!ModelState.IsValid) return BadRequest(user);

            try
            {
                _services.AddUserWithoutDuplicate(user);
                return Created($"api/Users/email/{user.Email}", user);
            }
            catch (Exception ex)
            { 
                return Unauthorized(ex.Message); 
            }

        }

        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest(user);

            user.Password = _services.CodifyPassword(user.Password);

            _repository.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete("delete/{idUser}")]
        [Authorize(Roles ="ADMINISTRATOR")]
        public IActionResult DeleteUser([FromRoute] int idUser)
        {
            _repository.DeleteUser(idUser);
            return NoContent();
        }

        [HttpGet("id/{user}")]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public IActionResult GetUserById([FromRoute] int iduser)
        {
            var user = _repository.GetUserById(iduser);

            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public IActionResult GetUserByName([FromQuery] string nameUser)
        {
            var users = _repository.GetUserByName(nameUser);
            if (users.Count < 1) return NoContent();
            return Ok(users);
        }

        [HttpGet("email/{emailUser}")]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public IActionResult GetUserByEmail([FromRoute] string emailUser)
        {
            var user = _repository.GetUserByEmail(emailUser);

            if (user == null) return NotFound();

            return Ok(user);
        }
        #endregion
    }
}

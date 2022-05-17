using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using BlogPessoal.src.models;

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

        /// <summary>
        /// Create new User
        /// </summary>
        /// <param name="user">AddUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        /// POST /api/Users
        /// {
        /// "name": "Choi Siwon",
        /// "email": "siwon@domain.com",
        /// "password": "134652",
        /// "photo": "URLPHOTO",
        /// "type": "NORMAL"
        /// }
        ///
        /// </remarks>
        /// <response code="201">Return created user</response>
        /// <response code="400">Request error</response>
        /// <response code="401">Email already registered</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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


        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="user">UpdateDTOUser</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        /// PUT /api/Users
        /// {
        /// "id": 1,
        /// "name": "Siwon Choi",
        /// "password": "134652",
        /// "photo": "URLUPDATEDPHOTO"
        /// }
        ///
        /// </remarks>
        /// <response code="200">Return updated user</response>
        /// <response code="400">Request error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> UpdateUser([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest(user);

            user.Password = _services.CodifyPassword(user.Password);

            await  _repository.UpdateUserAsync(user);
            return Ok(user);
        }


        /// <summary>
        /// Delete user by Id
        /// </summary>
        /// <param name="idUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">User deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idUser}")]
        [Authorize(Roles ="ADMINISTRATOR")]
        public async Task<ActionResult> DeleteUser([FromRoute] int idUser)
        {
            await  _repository.DeleteUserAsync(idUser);
            return NoContent();
        }


        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="iduser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="404">User does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{user}")]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByIdAsync([FromRoute] int iduser)
        {
            var user = await _repository.GetUserByIdAsync(iduser);

            if (user == null) return NotFound();
            return Ok(user);
        }


        /// <summary>
        /// Get user by name
        /// </summary>
        /// <param name="nameUser">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="204">Name does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByNameAsync([FromQuery] string nameUser)
        {
            var users = await _repository.GetUserByNameAsync(nameUser);
            if (users.Count < 1) return NoContent();
            return Ok(users);
        }


        /// <summary>
        /// Get user by Email
        /// </summary>
        /// <param name="emailUser">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="404">Email does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

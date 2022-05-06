using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Atributes

        private readonly IUser _repository;

        #endregion Atributes

        #region Constructors

        public UserController(IUser repository)

        { _repository = repository; }

        #endregion Constructors

        #region Methods

        [HttpPost]
        public IActionResult AddUser([FromBody] AddUserDTO user)
        {
            if(!ModelState.IsValid) return BadRequest(user);

            _repository.AddUser(user);
            return Created($"api/Users/{user.Email}", user);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest(user);

            _repository.UpdateUser(user);
            return Ok(user);
        }

        [HttpDelete("delete/{idUser}")]
        public IActionResult DeleteUser([FromRoute] int idUser)
        {
            _repository.DeleteUser(idUser);
            return NoContent();
        }

        [HttpGet("id/{user}")]
        public IActionResult GetUserById([FromRoute] int iduser)
        {
            var user = _repository.GetUserById(iduser);

            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet("email/{emailUser}")]
        public IActionResult GetUserByEmail([FromRoute] string emailUser)
        {
            var user = _repository.GetUserByEmail(emailUser);

            if (user == null) return NotFound();

            return Ok(user);
        }
        #endregion
    }
}

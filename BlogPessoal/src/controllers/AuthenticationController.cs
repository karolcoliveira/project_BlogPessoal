using System;
using BlogPessoal.src.dtos;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Authentication")]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        #region Atributes

        private readonly IAuthentication _services;

        #endregion

        #region Constructors

        public AuthenticationController(IAuthentication services)
        {
            _services = services;
        }
        #endregion

        #region Methods

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authentication([FromBody] AuthenticationDTO authentication)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var authorization = _services.GetAuthorization(authentication);
                return Ok(authorization);
            }

            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        #endregion
    }
}

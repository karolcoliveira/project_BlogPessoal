using System;
using BlogPessoal.src.dtos;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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


        /// <summary>
        /// Get Authorization
        /// </summary>
        /// <param name="authentication">AuthenticateDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        /// POST /api/Authentication
        /// {
        /// "email": "gustavo@domain.com",
        /// "password": "134652"
        /// }
        ///
        /// </remarks>
        /// <response code="201">Return created user</response>
        /// <response code="400">Request error</response>
        /// <response code="401">Invalid email or password</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AuthorizationDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Authentication([FromBody] AuthenticationDTO authentication)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                var authorization = await _services.GetAuthorizationAsync(authentication);
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

using System;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Themes")]
    [Produces("application/json")]
    public class ThemeController : ControllerBase
    {
        #region Atributes

        private readonly ITheme _repository;

        #endregion

        #region Constructors
        public ThemeController(ITheme repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods

        /// <summary>
        /// Get all themes
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">List of themes</response>
        /// <response code="204">Empty list</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("list")]
        [Authorize]
        public async Task<ActionResult> GetAllThemesAsync()
        {
            var list = await _repository.GetAllThemesAsync();
            if (list.Count < 1) return NoContent();
            return Ok(list);
        }


        /// <summary>
        /// Get theme by Id
        /// </summary>
        /// <param name="idTheme">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the theme</response>
        /// <response code="404">Theme does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idTheme}")]
        [Authorize]
        public async Task<ActionResult> GetThemeByIdAsync([FromRoute] int idTheme)
        {
            var theme = await _repository.GetThemeByIdAsync(idTheme);
            if (theme == null) return NotFound();
            return Ok(theme);
        }


        /// <summary>
        /// Get theme by Description
        /// </summary>
        /// <param name="descriptionTheme">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns themes</response>
        /// <response code="204">Description does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("description")]
        [Authorize]
        public async Task<ActionResult> GetThemeByDescriptionAsync([FromQuery] string descriptionTheme)
        {
            var themes = await _repository.GetThemeByDescriptionAsync(descriptionTheme);
            if (themes.Count < 1) return NoContent();
            return Ok(themes);
        }


        /// <summary>
        /// Create new Theme
        /// </summary>
        /// <param name="theme">NewDTOTheme</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        /// POST /api/Themes
        /// {
        /// "description": "Super Junior 17th anniversary",
        /// }
        ///
        /// </remarks>
        /// <response code="201">Returns created theme</response>
        /// <response code="400">Request error</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddThemeAsync([FromBody] AddThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repository.AddThemeAsync(theme);
            return Created($"api/Themes/id/{theme.Id}", theme);
        }


        /// <summary>
        /// Update Theme
        /// </summary>
        /// <param name="theme">UpdateDTOTheme</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        /// PUT /api/Themes
        /// {
        /// "id": 1,
        /// "description": "Suju 17th anniversary"
        /// }
        ///
        /// </remarks>
        /// <response code="200">Returns updated theme</response>
        /// <response code="400">Request error</response>
         [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
         [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> UpdateTheme([FromBody] UpdateThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
           await _repository.UpdateThemeAsync(theme);
            return Ok(theme);
        }


        /// <summary>
        /// Delete theme by Id
        /// </summary>
        /// <param name="idTheme">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Theme deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idTheme}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> DeleteTheme([FromRoute] int idTheme)
        {
           await _repository.DeleteThemeAsync(idTheme);
            return NoContent();
        }

        #endregion
    }
}

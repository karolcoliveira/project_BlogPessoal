using System;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Authorization;
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

        #region Methods

        [HttpGet("list")]
        [Authorize]
        public async Task<ActionResult> GetAllThemesAsync()
        {
            var list = await _repository.GetAllThemesAsync();
            if (list.Count < 1) return NoContent();
            return Ok(list);
        }

        [HttpGet("id/{idTheme}")]
        [Authorize]
        public async Task<ActionResult> GetThemeByIdAsync([FromRoute] int idTheme)
        {
            var theme = await _repository.GetThemeByIdAsync(idTheme);
            if (theme == null) return NotFound();
            return Ok(theme);
        }

        [HttpGet("description")]
        [Authorize]
        public async Task<ActionResult> GetThemeByDescriptionAsync([FromQuery] string descriptionTheme)
        {
            var themes = await _repository.GetThemeByDescriptionAsync(descriptionTheme);
            if (themes.Count < 1) return NoContent();
            return Ok(themes);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddThemeAsync([FromBody] AddThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repository.AddThemeAsync(theme);
            return Created($"api/Themes/id/{theme.Id}", theme);
        }

        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> UpdateTheme([FromBody] UpdateThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
           await _repository.UpdateThemeAsync(theme);
            return Ok(theme);
        }

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

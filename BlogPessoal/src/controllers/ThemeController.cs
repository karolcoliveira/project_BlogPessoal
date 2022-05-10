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

        [HttpGet]
        [Authorize]
        public IActionResult GetAllThemes()
        {
            var list = _repository.GetAllThemes();
            if (list.Count < 1) return NoContent();
            return Ok(list);
        }

        [HttpGet("id/{idTheme}")]
        [Authorize]
        public IActionResult GetThemeById([FromRoute] int idTheme)
        {
            var theme = _repository.GetThemeById(idTheme);
            if (theme == null) return NotFound();
            return Ok(theme);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetThemeByDescription([FromQuery] string descriptionTheme)
        {
            var themes = _repository.GetThemeByDescription(descriptionTheme);
            if (themes.Count < 1) return NoContent();
            return Ok(themes);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddTheme([FromBody] AddThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.AddTheme(theme);
            return Created($"api/Themes/id/{theme.Id}", theme);
        }

        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult UpdateTheme([FromBody] UpdateThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.UpdateTheme(theme);
            return Ok(theme);
        }

        [HttpDelete("delete/{idTheme}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult DeleteTheme([FromRoute] int idTheme)
        {
            _repository.DeleteTheme(idTheme);
            return NoContent();
        }

        #endregion
    }
}

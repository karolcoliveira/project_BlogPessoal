using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
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
        public IActionResult GetAllThemes()
        {
            var list = _repository.GetAllThemes();
            if (list.Count < 1) return NoContent();
            return Ok(list);
        }

        [HttpGet("id/{idTheme}")]
        public IActionResult GetThemeById([FromRoute] int idTheme)
        {
            var theme = _repository.GetThemeById(idTheme);
            if (theme == null) return NotFound();
            return Ok(theme);
        }

        [HttpGet]
        public IActionResult GetThemeByDescription([FromQuery] string descriptionTheme)
        {
            var themes = _repository.GetThemeByDescription(descriptionTheme);
            if (themes.Count < 1) return NoContent();
            return Ok(themes);
        }

        [HttpPost]
        public IActionResult AddTheme([FromBody] AddThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.AddTheme(theme);
            return Created($"api/Themes/id/{theme.Id}", theme);
        }

        [HttpPut]
        public IActionResult UpdateTheme([FromBody] UpdateThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.UpdateTheme(theme);
            return Ok(theme);
        }

        [HttpDelete("delete/{idTheme}")]
        public IActionResult DeleteTheme([FromRoute] int idTheme)
        {
            _repository.DeleteTheme(idTheme);
            return NoContent();
        }

        #endregion
    }
}

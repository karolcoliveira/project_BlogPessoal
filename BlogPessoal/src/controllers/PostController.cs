using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Posts")]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        #region Atributes

        private readonly IPost _repository;

        #endregion

        #region Constructors

        public PostController (IPost repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods

        [HttpGet("id/{idPost}")]
        [Authorize]
        public async Task<ActionResult> GetPostByIdAsync([FromRoute] int idPost)
        {
            var post = await _repository.GetPostByIdAsync(idPost);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            var list = await _repository.GetAllPostsAsync();
            if (list.Count < 1) return NoContent();
            return Ok(list);
        }

        [HttpGet("search")]
        [Authorize]
         public async Task<ActionResult> GetAllPostsBySearchAsync(
            [FromQuery] string title,
            [FromQuery] string descriptionTheme,
            [FromQuery] string nameCreator)
        {
            var list = await _repository.GetAllPostsBySearchAsync(title,descriptionTheme, nameCreator);

            if (list.Count < 1) return NoContent();
            return Ok(list);
        }

        [HttpPost]
        [Authorize]
         public async Task<ActionResult> AddPostAsync([FromBody] AddPostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repository.AddPostAsync(post);

            return Created($"api/Posts/id/{post.Id}", post);
        }

        [HttpPut]
        [Authorize]
         public async Task<ActionResult> UpdatePost([FromBody] UpdatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repository.UpdatePostAsync(post);
            return Ok(post);
        }

        [HttpDelete("delete/{idPost}")]
        [Authorize]
         public async Task<ActionResult> DeletePost([FromRoute] int idPost)
        {
            await _repository.DeletePostAsync(idPost);
            return NoContent();
        }

        #endregion
    }
}

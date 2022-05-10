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
        public IActionResult GetPostById([FromRoute] int idPost)
        {
            var post = _repository.GetPostById(idPost);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllPosts()
        {
            var list = _repository.GetAllPosts();
            if (list.Count < 1) return NoContent();
            return Ok(list);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAllPostBySearch(
            [FromQuery] string title,
            [FromQuery] string descriptionTheme,
            [FromQuery] string nameCreator)
        {
            var posts = _repository.GetPostBySearch(title,descriptionTheme, nameCreator);

            if (posts.Count < 1) return NoContent();
            return Ok(posts);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddPost([FromBody] AddPostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.AddPost(post);

            return Created($"api/Posts/id/{post.Id}", post);
        }

        [HttpPut]
        [Authorize]
        public IActionResult UpdatePost([FromBody] UpdatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.UpdatePost(post);
            return Ok(post);
        }

        [HttpDelete("delete/{idPost}")]
        [Authorize]
        public IActionResult DeletePost([FromRoute] int idPost)
        {
            _repository.DeletePost(idPost);
            return NoContent();
        }

        #endregion
    }
}

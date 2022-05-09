using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Posts")]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {
        #region Atibutes

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
        public IActionResult GetPostById([FromRoute] int idPost)
        {
            var post = _repository.GetPostById(idPost);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            var list = _repository.GetAllPosts();
            if (list.Count < 1) return NoContent();
            return Ok(list);
        }

        [HttpGet]
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
        public IActionResult AddPost([FromBody] AddPostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.AddPost(post);

            return Created($"api/Posts/id/{post.Id}", post);
        }

        [HttpPut]
        public IActionResult UpdatePost([FromBody] UpdatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.UpdatePost(post);
            return Ok(post);
        }

        [HttpDelete("delete/{idPost}")]
        public IActionResult DeletePost([FromRoute] int idPost)
        {
            _repository.DeletePost(idPost);
            return NoContent();
        }

        #endregion
    }
}

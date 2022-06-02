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

        /// <summary>
        /// Get post by Id
        /// </summary>
        /// <param name = "idPost">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return to post</response>
        /// <response code="404">Post does not exist</response>
        [HttpGet("id/{idPost}")]
        [Authorize]
        public async Task<ActionResult> GetPostByIdAsync([FromRoute] int idPost)
        {
            var post = await _repository.GetPostByIdAsync(idPost);
            if (post == null) return NotFound();
            return Ok(post);
        }
        
        
        /// <summary>
        /// Get all posts
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Post List</response>
        /// <response code="204">Empty list</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            var list = await _repository.GetAllPostsAsync();
            if (list.Count < 1) return NoContent();
            return Ok(list);
        }


        /// <summary>
        /// Get posts by search
        /// </summary>
        /// <param name="title">string</param>
        /// <param name="descriptionTheme">string</param>
        /// <param name="nameCreator">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return posts</response>
        /// <response code="204">Posts do not exist for this search</response>
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


        /// <summary>
        /// Add new Post
        /// </summary>
        /// <param name="post">AddPostDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        /// POST /api/Posts
        /// {
        /// "title": "Super Junior is so cool",
        /// "description": "They are celebrating their 17th aniversary this year",
        /// "photo": "IMAGEURL",
        /// "emailCreator": "siwon@email.com",
        /// "Theme description": "SuperJunior"
        /// }
        ///
        /// </remarks>
        /// <response code="201">Return created post</response>
        /// <response code="400">Request error</response>
        [HttpPost]
        [Authorize]
         public async Task<ActionResult> AddPostAsync([FromBody] AddPostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repository.AddPostAsync(post);

            return Created($"api/Posts/id/{post.Id}", post);
        }


        /// <summary>
        /// Update theme name
        /// </summary>
        /// <param name="post">UpdateDTOPost</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        /// PUT /api/Posts
        /// {
        /// "id": 1,
        /// "title": "Super Junior is so cool",
        /// "description": "They are celebrating their 17th aniversary this year",
        /// "photo": "IMAGEURLD",
        /// "Theme description": "Suju"
        /// }
        ///
        /// </remarks>
        /// <response code="200">Returns updated post</response>
        /// <response code="400">Request error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize]
         public async Task<ActionResult> UpdatePost([FromBody] UpdatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repository.UpdatePostAsync(post);
            return Ok(post);
        }


        /// <summary>
        /// Delete post by Id
        /// </summary>
        /// <param name="idPost">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Post deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
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

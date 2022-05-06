using System.Linq;
using System.Collections.Generic;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.repositories.implements
{
    public class PostRepository : IPost
    {
        #region Atributes

        private readonly BlogPessoalContext _context;

        #endregion Atributes

        #region Constructors

        public PostRepository(BlogPessoalContext context)

        { _context = context; }

        #endregion Constructors

        #region Methods
        public void AddPost(AddPostDTO post)
        {
            _context.Posts.Add(new PostModel
            {
                Title = post.Title,
                Description = post.Description,
                Photo = post.Photo,
                Creator = _context.Users.FirstOrDefault(
                     u => u.Email == post.EmailCreator),
                RelatedPosts = _context.Themes.FirstOrDefault(
                     t => t.Description == post.DescriptionTheme)
            });
            _context.SaveChanges();
        }

        public void DeletePost(int id)
        {
            _context.Posts.Remove(GetPostById(id));
            _context.SaveChanges();
        }

        public List<PostModel> GetAllPosts()
        {
            return _context.Posts.ToList();
        }

        public PostModel GetPostById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<PostModel> GetPostBySearch(string title, string descriptionTheme, string nameCreator)
        {
            switch (title, descriptionTheme, nameCreator)
            {
                case (null, null, null):
                    return GetAllPosts();

                case (null, null, _):
                    return _context.Posts
                        .Include(p => p.RelatedPosts)
                        .Include(p => p.Creator)
                        .Where(p => p.Creator.Name.Contains(nameCreator))
                        .ToList();

                case (null, _, null):
                    return _context.Posts
                        .Include(p => p.RelatedPosts)
                        .Include(p => p.Creator)
                        .Where(p => p.RelatedPosts.Description.Contains(descriptionTheme))
                        .ToList();

                case (_, null, null):
                    return _context.Posts
                        .Include(p => p.RelatedPosts)
                        .Include(p => p.Creator)
                        .Where(p => p.Title.Contains(title))
                        .ToList();

                case (_, _, null):
                    return _context.Posts
                         .Include(p => p.RelatedPosts)
                         .Include(p => p.Creator)
                         .Where(p => p.Title.Contains(title) & p.RelatedPosts.Description.Contains(descriptionTheme))
                        .ToList();

                case (null, _, _):
                    return _context.Posts
                        .Include(p => p.RelatedPosts)
                        .Include(p => p.Creator)
                        .Where(p => p.RelatedPosts.Description.Contains(descriptionTheme) & p.Creator.Name.Contains(nameCreator))
                        .ToList();

                case (_, null, _):
                    return _context.Posts
                        .Include(p => p.RelatedPosts)
                        .Include(p => p.Title)
                        .Where(p => p.Title.Contains(title) & p.Creator.Name.Contains(nameCreator))
                        .ToList();

                case (_, _, _):
                    return _context.Posts
                        .Include(p => p.RelatedPosts)
                        .Include(p => p.Title)
                        .Where(p => p.Title.Contains(title) | p.RelatedPosts.Description.Contains(descriptionTheme) | p.Creator.Name.Contains(nameCreator))
                        .ToList();
            }
        }

        public void UpdatePost(UpdatePostDTO post)
        {
            var oldPost = GetPostById(post.Id);
            oldPost.Title = post.Title;
            oldPost.Description = post.Description;
            oldPost.Photo = post.Photo;
            oldPost.RelatedPosts = _context.Themes.FirstOrDefault(t => t.Description == post.DescriptionTheme);
            _context.Posts.Update(oldPost);
            _context.SaveChanges();
        }
        #endregion Methods
    }
}

using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task AddPostAsync(AddPostDTO post)
        {
           await  _context.Posts.AddAsync(new PostModel
            {
                Title = post.Title,
                Description = post.Description,
                Photo = post.Photo,
                Creator = _context.Users.FirstOrDefault(
                     u => u.Email == post.EmailCreator),
                RelatedPosts = _context.Themes.FirstOrDefault(
                     t => t.Description == post.DescriptionTheme)
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
             _context.Posts.Remove(await GetPostByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<PostModel>> GetAllPostsAsync()
        {
             return await _context.Posts
                    .Include(p => p.Creator)
                    .Include(p => p.RelatedPosts)
                    .ToListAsync();
        }

        public async  Task<PostModel> GetPostByIdAsync(int id)
        {
             return await _context.Posts
                    .Include(p => p.Creator)
                    .Include(p => p.RelatedPosts)
                    .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PostModel>> GetAllPostsBySearchAsync(string title, string descriptionTheme, string nameCreator)
        {
            switch (title, descriptionTheme, nameCreator)
            {
                case (null, null, null):
                    return await GetAllPostsAsync();

                case (null, null, _):
                    return await _context.Posts
                            .Include(p => p.RelatedPosts)
                            .Include(p => p.Creator)
                            .Where(p => p.Creator.Name.Contains(nameCreator))
                            .ToListAsync();

                case (null, _, null):
                    return await _context.Posts
                            .Include(p => p.RelatedPosts)
                            .Include(p => p.Creator)
                            .Where(p => p.RelatedPosts.Description.Contains(descriptionTheme))
                            .ToListAsync();

                case (_, null, null):
                    return await _context.Posts
                            .Include(p => p.RelatedPosts)
                            .Include(p => p.Creator)
                            .Where(p => p.Title.Contains(title))
                            .ToListAsync();

                case (_, _, null):
                    return await _context.Posts
                            .Include(p => p.RelatedPosts)
                            .Include(p => p.Creator)
                            .Where(p => p.Title.Contains(title) 
                            & p.RelatedPosts.Description.Contains(descriptionTheme))
                            .ToListAsync();

                case (null, _, _):
                    return await _context.Posts
                            .Include(p => p.RelatedPosts)
                            .Include(p => p.Creator)
                            .Where(p => p.RelatedPosts.Description.Contains(descriptionTheme) 
                            & p.Creator.Name.Contains(nameCreator))
                            .ToListAsync();

                case (_, null, _):
                    return await _context.Posts
                            .Include(p => p.RelatedPosts)
                            .Include(p => p.Title)
                            .Where(p => p.Title.Contains(title) 
                            & p.Creator.Name.Contains(nameCreator))
                            .ToListAsync();

                case (_, _, _):
                    return await _context.Posts
                            .Include(p => p.RelatedPosts)
                            .Include(p => p.Title)
                            .Where(p => p.Title.Contains(title) 
                            | p.RelatedPosts.Description.Contains(descriptionTheme) 
                            | p.Creator.Name.Contains(nameCreator))
                            .ToListAsync();
            }
        }

        public async Task UpdatePostAsync(UpdatePostDTO post)
        {
            var oldPost = await GetPostByIdAsync(post.Id);
            oldPost.Title = post.Title;
            oldPost.Description = post.Description;
            oldPost.Photo = post.Photo;
            oldPost.RelatedPosts = _context.Themes.FirstOrDefault(t => t.Description == post.DescriptionTheme);

            _context.Posts.Update(oldPost);
            await _context.SaveChangesAsync();
        }

        #endregion Methods
        }
    }

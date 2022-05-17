using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.data
{
    /// <summary>
    /// <para>Summary: Context class, responsible for loading context and defining DbSets</para>
    /// <para>Created by: Karol Oliveira</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 12/05/2022</para>
    /// </summary>
    public class BlogPessoalContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }

        public DbSet<ThemeModel> Themes { get; set; }

        public DbSet<PostModel> Posts { get; set; }

        public BlogPessoalContext(DbContextOptions<BlogPessoalContext> opt) : base(opt)
        { 
        }
    }
}

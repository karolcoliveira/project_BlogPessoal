using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implements;
using BlogPessoal.src.utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoalTeste.Tests.repositories
{
    [TestClass]
    public class PostRepositoryTest
    {
        private BlogPessoalContext _context;

        private IUser _repositoryU;
        private ITheme _repositoryT;
        private IPost _repositoryP;


        [TestMethod]
        public async Task CreateThreePostsIntoSystemReturnThreeAsync()
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal22")
            .Options;

            _context = new BlogPessoalContext(opt);

            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            await _repositoryU.AddUserAsync(
                new AddUserDTO("Cho Kyuhyun","kyuhyun@email","200288","URLPHOTO", TypeUser.NORMAL));

           await _repositoryU.AddUserAsync(
                new AddUserDTO("Lee Donghae", "haefish@email", "121086", "URLPHOTO", TypeUser.NORMAL));

            await _repositoryT.AddThemeAsync(new AddThemeDTO("SuperJunior"));
            await _repositoryT.AddThemeAsync(new AddThemeDTO("Shinee"));
            await _repositoryT.AddThemeAsync(new AddThemeDTO("EXO"));
            await _repositoryT.AddThemeAsync(new AddThemeDTO("NCT"));

            // Given that I added 3 posts into database
            await _repositoryP.AddPostAsync(
                    new AddPostDTO
                    ("Super Junior is so cool", 
                    "They are celebrating their 17th aniversary this year",
                    "URLPHOTO",
                    "haefish@email.com",
                    "SuperJunior"));

            await _repositoryP.AddPostAsync(
                    new AddPostDTO
                    ("Super Junior's members are legends among idols",
                    "They still dance and sing just like 2005",
                    "URLPHOTO",
                    "kyuhyun@email",
                    "SuperJunior"));

            await _repositoryP.AddPostAsync(
                    new AddPostDTO
                    ("SHINee is so cool",
                    "They are also celebrating their 14th aniversary this year",
                    "URLPHOTO",
                    "haefish@email.com",
                    "SHINee"));

            //When searching all posts
            var posts = await _repositoryP.GetAllPostsAsync();

            //Then, it should return 3 posts
            Assert.AreEqual(3, posts.Count());
        }

        [TestMethod]
        public async Task UpdatePostReturnUpdatedPostAsync()
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal23")
            .Options;

            _context = new BlogPessoalContext(opt);

            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            //
            await _repositoryU.AddUserAsync(
                new AddUserDTO("Lee Donghae", "haefish@email", "121086", "URLPHOTO", TypeUser.NORMAL));
            
            //
            await _repositoryT.AddThemeAsync(new AddThemeDTO("SuperJunior"));
            await _repositoryT.AddThemeAsync(new AddThemeDTO("Shinee"));

            // Given that I added a post into database
            await _repositoryP.AddPostAsync(
                   new AddPostDTO
                   ("Super Junior is so cool",
                    "They are celebrating their 17th aniversary this year",
                    "URLPHOTO",
                    "haefish@email.com",
                    "SuperJunior"));

            //When updating post by their id
            await _repositoryP.UpdatePostAsync(
                    new UpdatePostDTO
                    (1,
                    "Super Junior is so cool",
                    "They stil dance and sing just like 2005",
                    "URLUPDATEDPHOTO",
                    "haefish@email.com",
                    "Suju"));

            var post = await _repositoryP.GetPostByIdAsync(1);

            //Then, it should return post with their information updated
            Assert.AreEqual("Super Junior is so cool", post.Title);

            Assert.AreEqual("They still dance and sing just like 2005", post.Description);

            Assert.AreEqual("URLUPDATEDPHOTO", post.Photo);

            Assert.AreEqual("Suju", post.DescriptionTheme);
        }

        [TestMethod]
        public async Task GetPostByCustomizedSearchAsync()

        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal24")
            .Options;

            _context = new BlogPessoalContext(opt);

            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            // Given that I added 2 users into database
            await _repositoryU.AddUserAsync(
                new AddUserDTO("Cho Kyuhyun","kyuhyun@email","200288","URLPHOTO", TypeUser.NORMAL));

           await _repositoryU.AddUserAsync(
                new AddUserDTO("Lee Donghae", "haefish@email", "121086", "URLPHOTO", TypeUser.NORMAL));

            // Given that I added 2 themes into database
            await _repositoryT.AddThemeAsync(new AddThemeDTO("SuperJunior"));
            await _repositoryT.AddThemeAsync(new AddThemeDTO("Shinee"));

            // Given that I added 3 posts into database
            await _repositoryP.AddPostAsync(
                   new AddPostDTO
                   ("Super Junior is so cool",
                    "They are celebrating their 17th aniversary this year",
                    "URLPHOTO",
                    "haefish@email.com",
                    "SuperJunior"));

            await _repositoryP.AddPostAsync(
                    new AddPostDTO
                    ("Super Junior's members are legends among idols",
                    "They still dance and sing just like 2005",
                    "URLPHOTO",
                    "kyuhyun@email",
                    "SuperJunior"));

            await _repositoryP.AddPostAsync(
                    new AddPostDTO
                    ("SHINee is so cool",
                    "They are also celebrating their 14th aniversary this year",
                    "URLPHOTO",
                    "haefish@email.com",
                    "SHINee"));

            var postTest1 = await _repositoryP.GetAllPostsBySearchAsync("cool", null, null);
            var postTest2 = await _repositoryP.GetAllPostsBySearchAsync(null, "SuperJunior", null);
            var postTest3 = await _repositoryP.GetAllPostsBySearchAsync(null, null, "Lee Donghae");

            //When seraching with customized filters
            //Then, it should return posts with correspondents criteria
            Assert.AreEqual(2, postTest1.Count());
            Assert.AreEqual(2, postTest2.Count());
            Assert.AreEqual(2, postTest3.Count());
        }
    }
}

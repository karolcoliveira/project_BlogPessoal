using System.Linq;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implements;
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
        public void CreateThreePostsIntoSystemReturnThree()
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal22")
            .Options;

            _context = new BlogPessoalContext(opt);

            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            _repositoryU.AddUser(
                new AddUserDTO(
                    "Cho Kyuhyun",
                    "kyuhyun@email",
                    "200288",
                    "URLPHOTO"));

            _repositoryU.AddUser(
                new AddUserDTO(
                    "Lee Donghae",
                    "haefish@email",
                    "121086",
                    "URLPHOTO"));

            _repositoryT.AddTheme(new AddThemeDTO("SuperJunior"));
            _repositoryT.AddTheme(new AddThemeDTO("Shinee"));
            _repositoryT.AddTheme(new AddThemeDTO("EXO"));
            _repositoryT.AddTheme(new AddThemeDTO("NCT"));

            // Given that I added 3 posts into database
            _repositoryP.AddPost(
                   new AddPostDTO
                   ("Super Junior is so cool",
                    "They are celebrating their 17th aniversary this year",
                    "URLPHOTO",
                    "haefish@email.com",
                    "SuperJunior"));

            _repositoryP.AddPost(
                    new AddPostDTO
                    ("Super Junior's members are legends among idols",
                    "They still dance and sing just like 2005",
                    "URLPHOTO",
                    "kyuhyun@email",
                    "SuperJunior"));

            _repositoryP.AddPost(
                    new AddPostDTO
                    ("SHINee is so cool",
                    "They are also celebrating their 14th aniversary this year",
                    "URLPHOTO",
                    "haefish@email.com",
                    "SHINee"));

            //When searching all posts
            _repositoryP.GetAllPosts();

            //Then, it should return 3 posts
            Assert.AreEqual(3, _repositoryP.GetAllPosts().Count());
        }

        [TestMethod]
        public void UpdatePostReturnUpdatedPost()
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal23")
            .Options;

            _context = new BlogPessoalContext(opt);

            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            _repositoryU.AddUser(
                new AddUserDTO(
                    "Cho Kyuhyun",
                    "kyuhyun@email",
                    "200288",
                    "URLPHOTO"));

            _repositoryU.AddUser(
                new AddUserDTO(
                    "Lee Donghae",
                    "haefish@email",
                    "121086",
                    "URLPHOTO"));

            _repositoryT.AddTheme(new AddThemeDTO("SuperJunior"));
            _repositoryT.AddTheme(new AddThemeDTO("Shinee"));
            _repositoryT.AddTheme(new AddThemeDTO("EXO"));
            _repositoryT.AddTheme(new AddThemeDTO("NCT"));

            // Given that I added 3 posts into database
            _repositoryP.AddPost(
                   new AddPostDTO
                   ("Super Junior is so cool",
                    "They are celebrating their 17th aniversary this year",
                    "URLPHOTO",
                    "haefish@email.com",
                    "SuperJunior"));

            _repositoryP.AddPost(
                    new AddPostDTO
                    ("Super Junior's members are legends among idols",
                    "They still dance and sing just like 2005",
                    "URLPHOTO",
                    "kyuhyun@email",
                    "SuperJunior"));

            _repositoryP.AddPost(
                    new AddPostDTO
                    ("SHINee is so cool",
                    "They are also celebrating their 14th aniversary this year",
                    "URLPHOTO",
                    "haefish@email.com",
                    "SHINee"));

            //When updating post title by their id
            _repositoryP.UpdatePost(
                    new UpdatePostDTO
                    (2,
                    "Super Junior is so cool",
                    "They still dance and sing just like 2005",
                    "URLUPDATEDPHOTO",
                    "SuperJunior"));

            //Then, it should return post with their information updated
            Assert.AreEqual("Super Junior is so cool", _repositoryP.GetPostById(2).Title);

            Assert.AreEqual("They still dance and sing just like 2005", _repositoryP.GetPostById(2).Description);

            Assert.AreEqual("URLUPDATEDPHOTO", _repositoryP.GetPostById(2).Photo);

            Assert.AreEqual("SuperJunior", _repositoryP.GetPostById(2).DescriptionTheme);
        }

        [TestMethod]
        public void GetPostByCustomizedSearch(
            string title,
            string descriptionTheme,
            string nameCreator)

        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal24")
            .Options;

            _context = new BlogPessoalContext(opt);

            _repositoryU = new UserRepository(_context);
            _repositoryT = new ThemeRepository(_context);
            _repositoryP = new PostRepository(_context);

            _repositoryU.AddUser(
                new AddUserDTO(
                    "Cho Kyuhyun",
                    "kyuhyun@email",
                    "200288",
                    "URLPHOTO"));

            _repositoryU.AddUser(
                new AddUserDTO(
                    "Lee Donghae",
                    "haefish@email",
                    "121086",
                    "URLPHOTO"));

            _repositoryT.AddTheme(new AddThemeDTO("SuperJunior"));
            _repositoryT.AddTheme(new AddThemeDTO("Shinee"));
            _repositoryT.AddTheme(new AddThemeDTO("EXO"));
            _repositoryT.AddTheme(new AddThemeDTO("NCT"));

            // Given that I added 3 posts into database
            _repositoryP.AddPost(
                   new AddPostDTO
                   ("Super Junior is so cool",
                    "They are celebrating their 17th aniversary this year",
                    "URLPHOTO",
                    "haefish@email.com",
                    "SuperJunior"));

            _repositoryP.AddPost(
                    new AddPostDTO
                    ("Super Junior's members are legends among idols",
                    "They still dance and sing just like 2005",
                    "URLPHOTO",
                    "kyuhyun@email",
                    "SuperJunior"));

            _repositoryP.AddPost(
                    new AddPostDTO
                    ("SHINee is so cool",
                    "They are also celebrating their 14th aniversary this year",
                    "URLPHOTO",
                    "haefish@email.com",
                    "SHINee"));

            //When seraching with customized filters
            var posts = _repositoryP.GetPostBySearch("cool", null, null);

            //Then, it should return 3 posts with cool in their description
            Assert.AreEqual(3, posts.Count());
        }
    }
}

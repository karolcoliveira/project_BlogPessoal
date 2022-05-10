using System.Linq;
using BlogPessoal.src.models;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BlogPessoalTeste.Tests.repositories
{
    [TestClass]
    public class ThemeRepositoryTest
    {
        private BlogPessoalContext _context;
        private ITheme _repository;

        [TestMethod]
        public void CreateFourThemesIntoDatabaseReturnFourThemes()
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal10")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            // Given that I added 4 themes into database
            _repository.AddTheme(new AddThemeDTO("SuperJunior"));
            _repository.AddTheme(new AddThemeDTO("SHINee"));
            _repository.AddTheme(new AddThemeDTO("Shinee"));
            _repository.AddTheme(new AddThemeDTO("GirlsDay"));

            //When searching all themes
            _repository.GetAllThemes();

            //Then, it should return 4 themes
            Assert.AreEqual(4, _repository.GetAllThemes().Count);
        }


        [TestMethod]
        public void GetThemeByIdReturnDescription(int id)
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal11")
            .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            // Given that I add a theme into database
            _repository.AddTheme(new AddThemeDTO("GirlsDay"));

            //When searching by their id
            _repository.GetThemeById(1);

            //Then, it should return description "GirlsDay"
            Assert.AreEqual("GirlsDay", _repository.GetThemeById(id).Description);
        }


        [TestMethod]
        public void GetThemeByDescriptionReturnTwoThemes(string description)
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal12")
            .Options;

            // Given that I added 4 themes into database
            _repository.AddTheme(new AddThemeDTO("SuperJunior"));
            _repository.AddTheme(new AddThemeDTO("SHINee"));
            _repository.AddTheme(new AddThemeDTO("Shinee"));
            _repository.AddTheme(new AddThemeDTO("GirlsDay"));

            //When searching by their descriptions
            var themes = _repository.GetThemeByDescription("ee");

            //Then, it should return 2 themes
            Assert.AreEqual(2, themes.Count());
        }


        [TestMethod]
        public void UpdateThemeSuperJuniorReturnSuju(int id)
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal13")
            .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            // Given that I add a theme into database
            _repository.AddTheme(new AddThemeDTO("SuperJunior"));

            //When updating that theme by their id
            _repository.UpdateTheme(new UpdateThemeDTO(1, "Suju"));

            //Then, it should return updated theme with "Suju" in their description
            Assert.AreEqual("Suju", _repository.GetThemeById(id).Description);
        }


        [TestMethod]
        public void DeleteThemesReturnNull(int id)
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal14")
            .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            // Given that I added 4 uthemes into database
            _repository.AddTheme(new AddThemeDTO("SuperJunior"));
            _repository.AddTheme(new AddThemeDTO("SHINee"));
            _repository.AddTheme(new AddThemeDTO("Shinee"));
            _repository.AddTheme(new AddThemeDTO("GirlsDay"));

            // When deleting themes by their ids
            _repository.DeleteTheme(id);

            //Then, it should return number 0
            Assert.AreEqual(0, _repository.GetAllThemes().Count);
        }
    }
}

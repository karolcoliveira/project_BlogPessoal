using System.Linq;
using System.Threading.Tasks;
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
        public async Task CreateFourThemesIntoDatabaseReturnFourThemes()
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal10")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            // Given that I added 4 themes into database
            await _repository.AddThemeAsync(new AddThemeDTO("SuperJunior"));
            await _repository.AddThemeAsync(new AddThemeDTO("SHINee"));
            await _repository.AddThemeAsync(new AddThemeDTO("Shinee"));
            await _repository.AddThemeAsync(new AddThemeDTO("GirlsDay"));

            //When searching all themes
            var themes = await _repository.GetAllThemesAsync();

            //Then, it should return 4 themes
            Assert.AreEqual(4, themes.Count);
        }


        [TestMethod]
        public async Task GetThemeByIdReturnTheme1(int id)
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal11")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            // Given that I add a theme into database
            await _repository.AddThemeAsync(new AddThemeDTO("GirlsDay"));

            //When searching by it's id
            var theme = await _repository.GetThemeByIdAsync(1);

            //Then, it should return description "GirlsDay"
            Assert.AreEqual("GirlsDay", theme.Description);
        }


        [TestMethod]
        public async Task GetThemeByDescriptionReturnTwoThemes(string description)
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal12")
                .Options;

            // Given that I added 2 themes into database
            await _repository.AddThemeAsync(new AddThemeDTO("SHINee"));

            await _repository.AddThemeAsync(new AddThemeDTO("Shinee"));

            //When searching by their descriptions
            var themes = await _repository.GetThemeByDescriptionAsync("ee");

            //Then, it should return 2 themes
            Assert.AreEqual(2, themes.Count());
        }

        [TestMethod]
        public async Task UpdateThemeSuperJuniorReturnSuju(int id)
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal13")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            // Given that I add a theme into database
            await _repository.AddThemeAsync(new AddThemeDTO("SuperJunior"));

            //When updating that theme by their id
            await _repository.UpdateThemeAsync(new UpdateThemeDTO(1, "Suju"));

            var theme = await _repository.GetThemeByIdAsync(1);

            //Then, it should return updated theme with "Suju" in their description
            Assert.AreEqual("Suju", theme.Description);
        }


        [TestMethod]
        public async Task DeleteThemesReturnNull(int id)
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal14")
                .Options;

            _context = new BlogPessoalContext(opt);
            _repository = new ThemeRepository(_context);

            // Given that I added a theme into database
            await _repository.AddThemeAsync(new AddThemeDTO("EXO"));

            // When deleting theme by it's ids
           await _repository.DeleteThemeAsync(1);

            //Then, it should return number 0
            Assert.IsNull(await _repository.GetThemeByIdAsync(1));
        }
    }
}

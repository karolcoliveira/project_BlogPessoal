using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoal.src.repositories.implements
{
    public class ThemeRepository : ITheme
    {
        #region Atributes

        private readonly BlogPessoalContext _context;

        #endregion Atributes

        #region Constructors

        public ThemeRepository(BlogPessoalContext context)

        { _context = context; }

        #endregion Constructors

        #region Methods
        public void AddTheme(AddThemeDTO theme)
        {
            _context.Themes.Add(new ThemeModel()
            { Description = theme.Description });
            _context.SaveChanges();
        }
        public void DeleteTheme(int id)
        {
            _context.Themes.Remove(GetThemeById(id));
            _context.SaveChanges();
        }

        public List<ThemeModel> GetAllThemes()
        {
            return _context.Themes.ToList();
        }

        public List<ThemeModel> GetThemeByDescription(string description)
        {
            return _context.Themes
                .Where(u => u.Description.Contains(description))
                .ToList();
        }
        public ThemeModel GetThemeById(int Id)
        {
           return _context.Themes.FirstOrDefault(u => u.Id == Id);
        }

        public void UpdateTheme(UpdateThemeDTO theme)
        {
            var oldTheme = GetThemeById(theme.Id);
            oldTheme.Description = theme.Description;
            _context.Themes.Update(oldTheme);
            _context.SaveChanges();
        }
        #endregion Methods
    }
}

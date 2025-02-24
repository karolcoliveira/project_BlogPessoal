﻿using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// <para>Summary: Asynchronous method to add a new theme</para>
        /// </summary>
        /// <param name="theme">AddThemeDTO</param>
        public async Task AddThemeAsync(AddThemeDTO theme)
        {
            await _context.Themes.AddAsync(new ThemeModel
            { 
                Description = theme.Description 
            });

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Summary: Asynchronous method to delete a theme</para>
        /// </summary>
        /// <param name="id"> ID</param>
        public async Task DeleteThemeAsync(int id)
        {
            _context.Themes.Remove(await GetThemeByIdAsync(id));

           await  _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Summary: Asynchronous method to get all themes</para>
        /// </summary>
        /// <return>ThemeModel List</return>
        public async Task<List<ThemeModel>> GetAllThemesAsync()
        {
            return await _context.Themes.ToListAsync();
        }

        /// <summary>
        /// <para>Summary: Asynchronous method to get themes by description</para>
        /// </summary>
        /// <param name="description">Description</param>
        /// <return>ThemeModel List</return>
        public async Task<List<ThemeModel>> GetThemeByDescriptionAsync(string description)
        {
            return await _context.Themes
                        .Where(u => u.Description.Contains(description))
                        .ToListAsync();
        }

        /// <summary>
        /// <para>Sumary: Asynchronous method to get a theme by Id</para>
        /// </summary>
        /// <param name="id">User ID</param>
        /// <return>ThemeModel</return>
        public async Task<ThemeModel> GetThemeByIdAsync(int id)
        {
           return await _context.Themes.FirstOrDefaultAsync(t => t.Id == id);
        }

        /// <summary>
        /// <para>Summary: Asynchronous method to update a theme</para>
        /// </summary>
        /// <param name="theme">UpdateThemeDTO</param>
        public async Task UpdateThemeAsync(UpdateThemeDTO theme)
        {
            var oldTheme = await GetThemeByIdAsync(theme.Id);
            oldTheme.Description = theme.Description;
            _context.Themes.Update(oldTheme);
            await _context.SaveChangesAsync();
        }
        #endregion Methods
    }
}

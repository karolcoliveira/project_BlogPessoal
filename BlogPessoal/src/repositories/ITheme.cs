using BlogPessoal.src.models;
using System.Collections.Generic;
using BlogPessoal.src.dtos;

namespace BlogPessoal.src.repositories
{/// <summary>
/// <para>Sumary: Interface to represent CRUD actions in themes</para>
/// <para>Criado por: Karol Oliveira</para>
/// <para>Versão: 1.0</para>
/// <para>Data: 29/04/2022</para>
/// </summary>
    public interface ITheme
    { 
        void AddTheme(AddThemeDTO theme);
        void  UpdateTheme(UpdateThemeDTO theme);
        void DeleteTheme(int id);
        ThemeModel GetThemeById(int id);
        List<ThemeModel> GetAllThemes();
        List<ThemeModel> GetThemeByDescription(string description);
    }
}
using System.ComponentModel.DataAnnotations;
using BlogPessoal.src.dtos;

namespace BlogPessoal.src.dtos
{/// <summary>
/// <para>Sumary: Mirror class to add a new post</para>
/// <para>Created by: Karol Oliveira</para>
/// <para>Version: 1.0</para>
/// <para>Date: 29/04/2022</para>
/// </summary>
    public class AddPostDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        public string Photo { get; set; }

        [Required]
        public string EmailCreator { get; set; }

        [Required]
        public string DescriptionTheme { get; set; }

        public AddPostDTO(string title, string description, string photo, string emailCreator, string descriptionTheme)
        {
            Title = title;
            Description = description;
            Photo = photo;
            EmailCreator = emailCreator;
            DescriptionTheme = descriptionTheme;
        }
    }
    /// <summary>
    /// <para>Sumary: Mirror class to update a theme</para>
    /// <para>Created by: Karol Oliveira</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public class UpdatePostDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(20)]
        public string Description { get; set; }

        [Required, StringLength(30)]
        public string Title { get; set; }

        public string Photo { get; set; }

        [Required]
        public string EmailCreator { get; set; }

        [Required]
        public string DescriptionTheme { get; set; }

        public UpdatePostDTO(string title, string description, string photo, string emailCreator, string descriptionTheme)
        {
  
            Title = title;
            Description = description;
            Photo = photo;
            EmailCreator = emailCreator;
            DescriptionTheme = descriptionTheme;
        }
    }
}
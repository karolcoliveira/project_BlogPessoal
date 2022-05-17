using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.src.models
{
        /// <summary>
     /// <para>Resume: Class responsible for representing tb_posts in the database.</para>
     /// <para>Created by: Karol Oliveira</para>
     /// <para>Version: 1.0</para>
     /// <para>Date: 12/05/2022</para>
     /// </summary>
    [Table("tb_posts")]
    public class PostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        [Required, StringLength(100)]
        public string DescriptionTheme { get; set; }

        public string Photo { get; set; }

        [ForeignKey("fk_user")]
        public UserModel Creator { get; set; }

        [ForeignKey("fk_theme")]
        public ThemeModel RelatedPosts { get; set; }

    }
}

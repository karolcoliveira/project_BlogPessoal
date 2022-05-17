using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.models
{
    /// <summary>
     /// <para>Resume: Class responsible for representing tb_themes in the database.</para>
     /// <para>Created by: Karol Oliveira</para>
     /// <para>Version: 1.0</para>
     /// <para>Date: 12/05/2022</para>
     /// </summary>
    [Table("tb_themes")]
    public class ThemeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Description { get; set; }

        [JsonIgnore]
        public List<PostModel> RelatedPosts { get; set; }
    }
}

﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using BlogPessoal.src.utilities;

namespace BlogPessoal.src.models
{
    /// <summary>
    /// <para>Resume: Class responsible for representing tb_users in the database.</para>
    /// <para>Created by: Karol Oliveira</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 12/05/2022</para>
    /// </summary>
    [Table("tb_users")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(30)]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        public string Photo { get; set; }

        [Required]
        public TypeUser Type { get; set; }

        [JsonIgnore]
        public List<PostModel> MyPosts { get; set; }

    }
}

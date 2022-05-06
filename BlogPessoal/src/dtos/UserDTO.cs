using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    /// <summary>
    /// <para>Sumary: Mirror class to add a new user</para>
    /// <para>Created by: Karol Oliveira</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public class AddUserDTO
    {
        private (string, string, string, string) p;

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(30)]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        public string Photo { get; set; }

        public AddUserDTO(string name, string email, string password, string photo)
        {
            Name = name;
            Email = email;
            Password = password;
            Photo = photo;
        }

        public AddUserDTO((string, string, string, string) p)
        {
            this.p = p;
        }
    }
    /// <summary>
    /// <para>Sumary: Mirror class to update an user</para>
    /// <para>Created by: Karol Oliveira</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public class UpdateUserDTO
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        public string Photo { get; set; }

        public UpdateUserDTO(int id, string name, string password, string photo)
        {
            Id = id;
            Name = name;
            Password = password;
            Photo = photo;
        }
    }
}

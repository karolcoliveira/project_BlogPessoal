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
        [Required, StringLength(50)]
        public string UserName { get; set; }

        [Required, StringLength(30)]
        public string Email { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        public string Photo { get; set; }

        public AddUserDTO(string username, string email, string password, string photo)
        {
            UserName = username;
            Email = email;
            Password = password;
            Photo = photo;
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
        [Required, StringLength(50)]
        public string UserName { get; set; }

        [Required, StringLength(30)]
        public string Password { get; set; }

        public string Photo { get; set; }

        public UpdateUserDTO(string username, string password, string photo)
        {
            UserName = username;
            Password = password;
            Photo = photo;
        }
    }
}

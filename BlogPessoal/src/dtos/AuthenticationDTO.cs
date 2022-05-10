﻿using BlogPessoal.src.utilities;
using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{
    public class AuthenticationDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public AuthenticationDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }

    public class AuthorizationDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public TypeUser Type { get; set; }
        public string Token { get; set; }
        public AuthorizationDTO(int id, string email, TypeUser type, string token)
        {
            Id = id;
            Email = email;
            Type = type;
            Token = token;
        }
    }
}

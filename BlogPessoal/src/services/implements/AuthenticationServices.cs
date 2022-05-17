using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogPessoal.src.services.implements
{
    /// <summary>
    /// <para>Summary: Class responsible for implementing IAuthentication</para>
    /// <para>Created by:Karol Oliveira</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 12/05/2022</para>
    /// </summary>
    public class AuthenticationServices : IAuthentication
    {
        #region Atributes
        private readonly IUser _repository;
        public IConfiguration Configuration { get; }

        #endregion

        #region Constructors
        public AuthenticationServices(IUser repository, IConfiguration configuration)
        {
            _repository = repository;
            Configuration = configuration;
        }

        #endregion

        #region Methods

        /// <summary>
        /// <para>Summary: Method responsible for encrypting password</para>
        /// </summary>
        /// <param name="password">Password to be encrypted</param>
        /// <returns>string</returns>
        public string CodifyPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        
        /// <summary>
        /// <para>Summary: Asynchronous method responsible for creating user without duplicating in the database</para>
        /// </summary>
        /// <param name="dto">AddUserDTO</param>
        public async Task AddUserWithoutDuplicateAsync(AddUserDTO dto)
        {
            var user = await _repository.GetUserByEmailAsync(dto.Email);

            if (user != null) throw new Exception("This email is already being used");

            dto.Password = CodifyPassword(dto.Password);

           await _repository.AddUserAsync(dto);
        }

        /// <summary>
        /// <para>Summary: Method responsible for generating JWT token</para>
        /// </summary>
        /// <param name="user">UserModel</param>
        /// <returns>string</returns>
        public string CreateToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                new Claim[]
            {
            new Claim(ClaimTypes.Email, user.Email.ToString()),
            new Claim(ClaimTypes.Role, user.Type.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                                    new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)};

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }


        /// <summary>
         /// <para>Summary: Asynchronous method responsible for returning authorization to authenticated user</para>
         /// </summary>
         /// <param name="authentication">AuthenticationDTO</param>
         /// <returns>DTO Authorization</returns>
         /// <exception cref="Exception">User not found</exception>
         /// <exception cref="Exception">Incorrect password</exception>
       public async Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO authentication)
        {
            var user = await _repository.GetUserByEmailAsync(authentication.Email);

            if (user == null) throw new Exception("User not found");

            if (user.Password != CodifyPassword(authentication.Password)) throw new Exception("Incorrect password");

            return new AuthorizationDTO(user.Id, user.Email, user.Type, CreateToken(user));
        }

        #endregion
    }
}

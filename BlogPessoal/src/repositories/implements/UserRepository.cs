using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories.implements
{
    public class UserRepository : IUser

    {
        #region Atributes

        private readonly BlogPessoalContext _context;

        #endregion Atributes

        #region Constructors

        public UserRepository(BlogPessoalContext context)

        {
            _context = context; 
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// <para>Summary: Asynchronous method to add a new user</para>
        /// </summary>
        /// <param name="user">AddUserDTO</param>
        public async Task AddUserAsync(AddUserDTO user)
        {
            await _context.Users.AddAsync(new UserModel
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Photo = user.Photo,
                Type = user.Type
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Summary: Asynchronous method to delete an user</para>
        /// </summary>
        /// <param name="id">User ID</param>
        public async Task DeleteUserAsync(int id)
        {
            _context.Users.Remove(await GetUserByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Summary: Asynchronous method to get a user by email</para>
        /// </summary>
        /// <param name="email">User email</param>
        /// <return>UserModel</return>
        public async Task<UserModel> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
         /// <para>Sumary: Asynchronous method to get a user by Id</para>
         /// </summary>
         /// <param name="id">User ID</param>
         /// <return>UserModel</return>
        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        }

        /// <summary>
         /// <para>Summary: Asynchronous method to get users by name</para>
         /// </summary>
         /// <param name="name">Username</param>
         /// <return>UserModel List</return>
        public async Task<List<UserModel>> GetUserByNameAsync(string name)
        {
            return await _context.Users
                .Where(u => u.Name.Contains(name))
                .ToListAsync();
        }

        /// <summary>
        /// <para>Summary: Asynchronous method to update a user</para>
        /// </summary>
        /// <param name="user">UpdateUserDTO</param>
        public async Task UpdateUserAsync(UpdateUserDTO user)
        {
            var oldUser = await GetUserByIdAsync(user.Id);
            oldUser.Name = user.Name;
            oldUser.Password = user.Password;
            oldUser.Photo = user.Photo;
            _context.Users.Update(oldUser);
           await  _context.SaveChangesAsync();
        }
        #endregion Methods
    }
}

using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BlogPessoal.src.repositories
{   /// <summary>
    /// <para>Sumary: Interface to represent CRUD actions in users</para>
    /// <para>Created by: Karol Oliveira</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public interface IUser
    {
        Task AddUserAsync(AddUserDTO user);
        Task UpdateUserAsync(UpdateUserDTO user);
        Task DeleteUserAsync(int id);
        Task <UserModel> GetUserByIdAsync(int id);
        Task <UserModel> GetUserByEmailAsync(string email);
      Task <List<UserModel>> GetUserByNameAsync(string user);
    }
}

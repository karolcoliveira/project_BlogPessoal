using BlogPessoal.src.dtos;
using BlogPessoal.src.models;

namespace BlogPessoal.src.repositories
{   /// <summary>
    /// <para>Sumary: Interface to represent CRUD actions in users</para>
    /// <para>Created by: Karol Oliveira</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public interface IUser
    {
        void AddUser(AddUserDTO username);
        void UpdateUser(UpdateUserDTO username);
        void DeleteUser(int id);
        UserModel GetUserById(int id);
        UserModel GetUserByEmail(string email);
        UserModel GetUserByUsername(string username);
    }
}

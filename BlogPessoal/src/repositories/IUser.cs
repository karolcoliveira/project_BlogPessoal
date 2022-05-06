using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Linq;


namespace BlogPessoal.src.repositories
{   /// <summary>
    /// <para>Sumary: Interface to represent CRUD actions in users</para>
    /// <para>Created by: Karol Oliveira</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public interface IUser
    {
        void AddUser(AddUserDTO user);
        void UpdateUser(UpdateUserDTO user);
        void DeleteUser(int id);
        UserModel GetUserById(int id);
        UserModel GetUserByEmail(string email);
       List<UserModel> GetUserByName(string user);
    }
}

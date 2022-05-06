﻿using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoal.src.repositories.implements
{
    public class UserRepository : IUser

    {
        #region Atributes

        private readonly BlogPessoalContext _context;

        #endregion Atributes

        #region Constructors

        public UserRepository(BlogPessoalContext context)

        {_context = context; }

        #endregion Constructors

        #region Methods

        public void AddUser(AddUserDTO user)
        {
            _context.Users.Add(new UserModel
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Photo = user.Photo
            });
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            _context.Users.Remove(GetUserById(id));
            _context.SaveChanges();
        }

        public UserModel GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public UserModel GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public UserModel GetUserByName(string name)
        {
            return _context.Users.FirstOrDefault(u => u.Name == name);
        }

        public void UpdateUser(UpdateUserDTO user)
        {
            var oldUser = GetUserById(user.Id);
            oldUser.Name = user.Name;
            oldUser.Password = user.Password;
            oldUser.Photo = user.Photo;
            _context.Users.Update(oldUser);
            _context.SaveChanges();
        }
        List<UserModel> IUser.GetUserByName(string user)
        {
            return _context.Users.ToList();
        }
        #endregion Methods
    }
}

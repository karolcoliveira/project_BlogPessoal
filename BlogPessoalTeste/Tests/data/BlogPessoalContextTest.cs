using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using BlogPessoal.src.data;
using BlogPessoal.src.models;
using System.Linq;
using System;
using BlogPessoal.src.utilities;

namespace BlogPessoalTeste.Tests.data

{
    [TestClass]
    public class BlogPessoalContextTest
    {
        private BlogPessoalContext _context;

        [TestInitialize]
        public void start()
        {
            var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
                .UseInMemoryDatabase(databaseName: "db_blogpessoal")
                .Options;

            _context = new BlogPessoalContext(opt);
        }

        [TestMethod]
        public void insertNewUserIntoDataReturnUser()

        {
            UserModel user = new UserModel();

            user.Name = "Karol Oliveira";
            user.Email = "karol@email.com";
            user.Password = "134652";
            user.Photo = "AKITAOLINKDAFOTO";
            user.Type = TypeUser.NORMAL;

            _context.Users.Add(user); // Add user

            _context.SaveChanges(); // Save changes

            Assert.IsNotNull(_context.Users.FirstOrDefault(u => u.Email == "karol@email.com"));

        }
    }
}

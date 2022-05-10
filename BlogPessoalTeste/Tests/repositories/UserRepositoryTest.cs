using System.Linq;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implements;
using BlogPessoal.src.utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoalTeste.Tests.repositories
{
	[TestClass]
	public class UserRepositoryTest
	{
		private BlogPessoalContext _context;
		private IUser _repository;


		[TestMethod]
		public void CreateFourUsersIntoDatabaseReturnFourUsers()
		{
			// Defining context
			var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
			.UseInMemoryDatabase(databaseName: "db_blogpessoal")
			.Options;

			_context = new BlogPessoalContext(opt);
			_repository = new UserRepository(_context);

			// Given that I added 4 users into database
			_repository.AddUser(
				new AddUserDTO(
					"Cho Kyuhyun",
					"kyuhyun@email",
					"200288",
					"URLPHOTO",
					TypeUser.NORMAL));

			_repository.AddUser(
				new AddUserDTO(
					"Lee Donghae",
					"haefish@email",
					"121086",
					"URLPHOTO",
					TypeUser.NORMAL));

			_repository.AddUser(
				new AddUserDTO(
					"Choi Siwon",
					"siwon@email.com",
					"134652",
					"URLFOTO",
					TypeUser.NORMAL));

			_repository.AddUser(
				new AddUserDTO(
					"Shin Donghee",
					"shindong@email.com",
					"134652",
					"URLFOTO",
					TypeUser.NORMAL));

			//When searching full list
			//Then, it should return 4 users
			Assert.AreEqual(4, _context.Users.Count());

		}

		[TestMethod]
		public  void GetUserByEmailReturnNotNull()
		{
			// Defining context
			var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
			.UseInMemoryDatabase(databaseName: "db_blogpessoal1")
			.Options;

			_context = new BlogPessoalContext(opt);
			_repository = new UserRepository(_context);

			// Given that I added an user into database

			_repository.AddUser(
				new AddUserDTO(
				"Shin Donghee",
				"shindong@email.com",
				"134652",
				"URLFOTO",
				TypeUser.NORMAL));

			//When searching this user's email
				var user = _repository.GetUserByEmail("shindong@email.com");

			//Then, it shuold return an user
				Assert.IsNotNull(user);
		}

		[TestMethod]
		public  void GetUserByIdReturnNotNullName()
		{
			// Defining context
			var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
			.UseInMemoryDatabase(databaseName: "db_blogpessoal2")
			.Options;

			_context = new BlogPessoalContext(opt);
			_repository = new UserRepository(_context);

			// Given that I added an user into database
			_repository.AddUser(
				new AddUserDTO(
					"Choi Siwon",
					"siwon@email.com",
					"134652",
					"URLFOTO",
					TypeUser.NORMAL));

			//When searching by id number 1
			var user = _repository.GetUserById(1);

			//Then, it should return an user
			Assert.IsNotNull(user);

			//Then, the user's name should be Choi Siwon
			Assert.AreEqual("Choi Siwon", user.Name);
		}

		[TestMethod]
		public void UpdateUserReturnUpdatedUser()
		{
			// Defining context
			var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
				.UseInMemoryDatabase(databaseName: "db_blogpessoal3")
				.Options;

				_context = new BlogPessoalContext(opt);
				_repository = new UserRepository(_context);

			// Given that I added an user into database
				_repository.AddUser(
						new AddUserDTO(
						"Park Jungsoo",
						"leeteuk@email.com",
						"134652",
						"URLFOTO",
						TypeUser.NORMAL));

			//When updating user's name and password
			var old = _repository.GetUserByEmail("leeteuk@email.com");
					_repository.UpdateUser(
						new UpdateUserDTO(1,
						"Park Leeteuk",
						"123456",
						"URLFOTONOVA"));

			// Then, when we validate the search, it should return name Park Leeteuk
				Assert.AreEqual("Park Leeteuk", _context.Users.FirstOrDefault(u => u.Id == old.Id).Name);

			// Then, when we validate the search, it should return password 123456
			Assert.AreEqual("123456", _context.Users.FirstOrDefault(u => u.Id == old.Id).Password);
		}
	}
}
using System.Linq;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoalTeste.Tests.repositories
{
	[TestClass]
	public class UserRepositoryTest
	{
		private BlogPessoalContext _context;
		private IUser _repository;

		[TestInitialize]
		public void InitialSetting()
		{
			var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
			.UseInMemoryDatabase(databaseName: "db_blogpessoal")
			.Options;

			_context = new BlogPessoalContext(opt);
			_repository = new UserRepository(_context);
		}

		[TestMethod]
		public void CreateFourUsersIntoDatabaseReturnFourUsers()
		{
			//GIVEN - Dado que registro 4 usuarios no banco

			_repository.AddUser(
				new AddUserDTO(
				"Gustavo Boaz",
				"gustavo@email.com",
				"134652",
				"URLFOTO"));

			_repository.AddUser(
			 new AddUserDTO(
				"Mallu Boaz",
				"mallu@email.com",
				"134652",
				"URLFOTO"));

			_repository.AddUser(
				new AddUserDTO(
				"Catarina Boaz",
				"catarina@email.com",
				"134652",
				"URLFOTO"));

			_repository.AddUser(
				new AddUserDTO(
				"Pamela Boaz",
				"pamela@email.com",
				"134652",
				"URLFOTO"));

			//WHEN - Quando pesquiso lista total
			//THEN - Então recebo 4 usuarios
			Assert.AreEqual(4, _context.Users.Count());

		}

		[TestMethod]
		public  void GetUserByEmailReturnNotNull()
		{
			//GIVEN - Dado que registro um usuario no banco

			_repository.AddUser(
				new AddUserDTO(
				"Zenildo Boaz",
				"zenildo@email.com",
				"134652",
				"URLFOTO"));

			//WHEN - Quando pesquiso pelo email deste usuario
				var user = _repository.GetUserByEmail("zenildo@email.com");

			//THEN - Então obtenho um usuario
				Assert.IsNotNull(user);
		}

		[TestMethod]
		public  void GetUserByIdReturnNotNullName()
		{
			//GIVEN - Dado que registro um usuario no banco
			_repository.AddUser(
				new AddUserDTO(
				"Neusa Boaz",
				"neusa@email.com",
				"134652",
				"URLFOTO"));

			//WHEN - Quando pesquiso pelo id 6
			var user = _repository.GetUserById(6);

			//THEN - Então, deve me retornar um elemento não nulo
			Assert.IsNotNull(user);

			//THEN - Então, o elemento deve ser Neusa Boaz
			Assert.AreEqual("Neusa Boaz", user.Name);
		}

			[TestMethod]
			public void UpdateUserReturnUpdatedUser()
			{
				//GIVEN - Dado que registro um usuario no banco
				_repository.AddUser(
					new AddUserDTO(
					"Estefânia Boaz",
					"estefania@email.com",
					"134652",
					"URLFOTO"));

			//WHEN - Quando atualizamos o usuario
			var old = _repository.GetUserByEmail("estefania@email.com");
				_repository.UpdateUser(
					new UpdateUserDTO(7,
					"Estefânia Moura",
					"123456",
					"URLFOTONOVA"));

			// THEN - Então, quando validamos pesquisa deve retornar nome Estefânia Moura

			Assert.AreEqual("Estefânia Moura", _context.Users.FirstOrDefault(u => u.Id == old.Id).Name);

			//THEN - Então, quando validamos pesquisa deve retornar senha 123456

			Assert.AreEqual("123456", _context.Users.FirstOrDefault(u => u.Id == old.Id).Password);
			}
	}
}
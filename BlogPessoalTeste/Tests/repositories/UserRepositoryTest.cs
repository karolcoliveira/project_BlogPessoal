using System.Linq;
using System.Threading.Tasks;
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
		public async Task CreateFourUsersIntoDatabaseReturnFourUsers()
		{
			// Defining context
			var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
			.UseInMemoryDatabase(databaseName: "db_blogpessoal")
			.Options;

			_context = new BlogPessoalContext(opt);
			_repository = new UserRepository(_context);

			// Given that I added 4 users into database
			await _repository.AddUserAsync(
				new AddUserDTO("Cho Kyuhyun", "kyuhyun@email", "200288", "URLPHOTO",TypeUser.NORMAL));

			await _repository.AddUserAsync(
				new AddUserDTO("Lee Donghae", "haefish@email", "121086", "URLPHOTO", TypeUser.NORMAL));

			await _repository.AddUserAsync(
				new AddUserDTO("Choi Siwon", "siwon@email.com","134652", "URLFOTO", TypeUser.NORMAL));

			await _repository.AddUserAsync(
				new AddUserDTO("Shin Donghee", "shindong@email.com", "134652", "URLFOTO", TypeUser.NORMAL));

			//When searching full list
			//Then, it should return 4 users
			Assert.AreEqual(4, _context.Users.Count());
		}

		[TestMethod]
		public async Task GetUserByEmailReturnNotNull()
		{
			// Defining context
			var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
			.UseInMemoryDatabase(databaseName: "db_blogpessoal1")
			.Options;

			_context = new BlogPessoalContext(opt);
			_repository = new UserRepository(_context);

			// Given that I added an user into database
			await _repository.AddUserAsync(
				new AddUserDTO("Shin Donghee", "shindong@email.com", "134652", "URLFOTO", TypeUser.NORMAL));

			//When searching this user's email
				var user = await _repository.GetUserByEmailAsync("shindong@email.com");

			//Then, it shuold return an user
				Assert.IsNotNull(user);
		}

		[TestMethod]
		public  async Task GetUserByIdReturnNotNullName()
		{
			// Defining context
			var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
			.UseInMemoryDatabase(databaseName: "db_blogpessoal2")
			.Options;

			_context = new BlogPessoalContext(opt);
			_repository = new UserRepository(_context);

			// Given that I added an user into database
			await _repository.AddUserAsync(
				new AddUserDTO("Choi Siwon", "siwon@email.com", "134652", "URLFOTO", TypeUser.NORMAL));

			//When searching by id number 1
			var user = await  _repository.GetUserByIdAsync(1);

			//Then, it should return an user
			Assert.IsNotNull(user);

			//Then, the user's name should be Choi Siwon
			Assert.AreEqual("Choi Siwon", user.Name);
		}

		[TestMethod]
		public async Task UpdateUserReturnUpdatedUser()
		{
			// Defining context
			var opt = new DbContextOptionsBuilder<BlogPessoalContext>()
				.UseInMemoryDatabase(databaseName: "db_blogpessoal3")
				.Options;

				_context = new BlogPessoalContext(opt);
				_repository = new UserRepository(_context);

			// Given that I added an user into database
			await _repository.AddUserAsync(
				new AddUserDTO("Park Jungsoo", "leeteuk@email.com", "134652", "URLFOTO", TypeUser.NORMAL));

			//When updating user's name and password
			await _repository.UpdateUserAsync(
				new UpdateUserDTO(1,"Park Leeteuk", "123456","URLNEWPHOTO"));

			// Then, when we validate the search, it should return name Park Leeteuk
			var old = await _repository.GetUserByEmailAsync("leeteuk@email.com");

			Assert.AreEqual("Park Leeteuk", _context.Users.FirstOrDefault(u => u.Id == old.Id).Name);

			// Then, when we validate the search, it should return password 123456
			Assert.AreEqual("123456", _context.Users.FirstOrDefault(u => u.Id == old.Id).Password);
		}
	}
}
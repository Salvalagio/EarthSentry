using EarthSentry.Contracts.Contracts.Users.Dtos;
using EarthSentry.Domain.Business;
using EarthSentry.Domain.Entities.Roles;
using EarthSentry.Domain.Entities.Users;
using EarthSentry.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EarthSentry.Test
{
    public class UserBusinessTests
    {
        private readonly Mock<ILogger<UserBusiness>> _loggerMock = new();
        private readonly Mock<IUserRepository> _userRepoMock = new();
        private readonly Mock<IRoleRepository> _roleRepoMock = new();

        private UserBusiness CreateUserBusinessMocked() =>
            new(_loggerMock.Object, _userRepoMock.Object, _roleRepoMock.Object);

        [Fact]
        public async Task GetAllUsersAsync_ReturnsUserDtos()
        {
            var users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    Username = "user1",
                    UserRoles = new List<UserRole>
                    {
                        new UserRole { Role = new Role { RoleName = "Admin" } }
                    }
                }
            };
            _userRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.GetAllUsersAsync();

            Assert.Single(result);
            Assert.Equal("user1", result.First().Username);
            Assert.Contains("Admin", result.First().Roles);
        }

        [Fact]
        public async Task GetUserByIdAsync_InvalidId_ReturnsEmptyDto()
        {
            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.GetUserByIdAsync("abc", "pass");
            Assert.Equal(0, result.UserId);
        }

        [Fact]
        public async Task GetUserByIdAsync_UserNotFound_ReturnsEmptyDto()
        {
            _userRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.GetUserByIdAsync("1", "pass");
            Assert.Equal(0, result.UserId);
        }

        [Fact]
        public async Task GetUserByIdAsync_WrongPassword_ReturnsEmptyDto()
        {
            var user = new User { UserId = 1, PasswordHash = "hash", UserRoles = new List<UserRole>() };
            _userRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);

            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.GetUserByIdAsync("1", "wrongpass");
            Assert.Equal(0, result.UserId);
        }

        [Fact]
        public async Task GetUserByIdAsync_Valid_ReturnsDto()
        {
            var password = "pass";
            var user = new User
            {
                UserId = 1,
                Username = "user",
                PasswordHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(password))),
                Email = "email",
                ImageUrl = "img",
                UserRoles = new List<UserRole> { new UserRole { Role = new Role { RoleName = "User" } } }
            };
            _userRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);

            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.GetUserByIdAsync("1", password);

            Assert.Equal(1, result.UserId);
            Assert.Equal("user", result.Username);
            Assert.Contains("User", result.Roles);
        }

        [Fact]
        public async Task DeleteUserAsync_InvalidId_ReturnsFalse()
        {
            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.DeleteUserAsync("abc", "pass");
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteUserAsync_UserNotFound_ReturnsFalse()
        {
            _userRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.DeleteUserAsync("1", "pass");
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteUserAsync_WrongPassword_ReturnsFalse()
        {
            var user = new User { UserId = 1, PasswordHash = "hash" };
            _userRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);

            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.DeleteUserAsync("1", "wrongpass");
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteUserAsync_Valid_UpdatesAndReturnsTrue()
        {
            var password = "pass";
            var user = new User
            {
                UserId = 1,
                PasswordHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(password))),
                IsActive = true
            };
            _userRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);
            _userRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.DeleteUserAsync("1", password);

            Assert.True(result);
            Assert.False(user.IsActive);
            _userRepoMock.Verify(r => r.Update(user), Times.Once);
            _userRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task LoginUserAsync_UserNotFound_ReturnsFalse()
        {
            _userRepoMock.Setup(r => r.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.LoginUserAsync(new UserLoginDto { Username = "user", Password = "pass" });

            Assert.False(result.Item1);
            Assert.Null(result.Item2);
            Assert.Equal(string.Empty, result.Item3);
        }

        [Fact]
        public async Task LoginUserAsync_WrongPassword_ReturnsFalse()
        {
            var user = new User { Username = "user", PasswordHash = "hash" };
            _userRepoMock.Setup(r => r.GetByUsernameAsync("user")).ReturnsAsync(user);

            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.LoginUserAsync(new UserLoginDto { Username = "user", Password = "wrongpass" });

            Assert.False(result.Item1);
        }

        [Fact]
        public async Task LoginUserAsync_Valid_UpdatesAndReturnsTrue()
        {
            var password = "pass";
            var user = new User
            {
                UserId = 1,
                Username = "user",
                PasswordHash = Convert.ToBase64String(System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(password))),
                ImageUrl = "img"
            };
            _userRepoMock.Setup(r => r.GetByUsernameAsync("user")).ReturnsAsync(user);
            _userRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.LoginUserAsync(new UserLoginDto { Username = "user", Password = password });

            Assert.True(result.Item1);
            Assert.Equal(1, result.Item2);
            Assert.Equal("img", result.Item3);
            _userRepoMock.Verify(r => r.Update(user), Times.Once);
            _userRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task RegisterUserAsync_UsernameExists_ReturnsFalse()
        {
            _userRepoMock.Setup(r => r.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(new User());

            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.RegisterUserAsync(new UserRegisterDto { Username = "user" });

            Assert.False(result.Success);
            Assert.Equal("Username already in use, try another.", result.Message);
        }

        [Fact]
        public async Task RegisterUserAsync_Valid_AddsUserAndRole_ReturnsTrue()
        {
            _userRepoMock.Setup(r => r.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User)null);
            _userRepoMock.Setup(r => r.AddAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            _userRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);
            _roleRepoMock.Setup(r => r.GetByNameAsync("User")).ReturnsAsync(new Role { RoleId = 1, RoleName = "User" });

            var userBusinessMocked = CreateUserBusinessMocked();
            var dto = new UserRegisterDto { Username = "user", Email = "email", Password = "pass", ImageUrl = "img" };
            var result = await userBusinessMocked.RegisterUserAsync(dto);

            Assert.True(result.Success);
            Assert.Equal("User has been created.", result.Message);
            _userRepoMock.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
            _userRepoMock.Verify(r => r.SaveAsync(), Times.Exactly(2));
        }

        [Fact]
        public async Task RegisterUserAsync_Exception_LogsErrorAndReturnsFalse()
        {
            _userRepoMock.Setup(r => r.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User)null);
            _userRepoMock.Setup(r => r.AddAsync(It.IsAny<User>())).ThrowsAsync(new Exception("fail"));

            var userBusinessMocked = CreateUserBusinessMocked();
            var dto = new UserRegisterDto { Username = "user", Email = "email", Password = "pass", ImageUrl = "img" };
            var result = await userBusinessMocked.RegisterUserAsync(dto);

            Assert.False(result.Success);
            Assert.Equal("Unknown error ocurred, please call the system admin.", result.Message);
        }

        [Fact]
        public async Task UpdateUserAsync_UserNotFound_ReturnsFalse()
        {
            _userRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((User)null);

            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.UpdateUserAsync(new UserUpdateDto { UserId = 1 });

            Assert.False(result);
        }

        [Fact]
        public async Task UpdateUserAsync_Valid_UpdatesAndReturnsTrue()
        {
            var user = new User { UserId = 1, Email = "old", ImageUrl = "oldimg", PasswordHash = "oldhash" };
            _userRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(user);
            _userRepoMock.Setup(r => r.SaveAsync()).Returns(Task.CompletedTask);

            var dto = new UserUpdateDto { UserId = 1, Email = "new", ImageUrl = "newimg", NewPassword = "newpass" };
            var userBusinessMocked = CreateUserBusinessMocked();
            var result = await userBusinessMocked.UpdateUserAsync(dto);

            Assert.True(result);
            Assert.Equal("new", user.Email);
            Assert.Equal("newimg", user.ImageUrl);
            Assert.NotEqual("oldhash", user.PasswordHash);
            _userRepoMock.Verify(r => r.Update(user), Times.Once);
            _userRepoMock.Verify(r => r.SaveAsync(), Times.Once);
        }
    }
}

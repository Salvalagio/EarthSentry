﻿using EarthSentry.Contracts.Contracts.Users.Dtos;
using EarthSentry.Contracts.Interfaces.Business;
using EarthSentry.Domain.Entities.Users;
using EarthSentry.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;
using System.Text;

namespace EarthSentry.Domain.Business
{
    public class UserBusiness(ILogger<UserBusiness> _logger,IUserRepository _userRepo, IRoleRepository _roleRepo) : IUserBusiness
    {
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users.Select(u => new UserDto
            {
                UserId = u.UserId,
                Username = u.Username,
                Roles = [.. u.UserRoles.Select(ur => ur.Role.RoleName)]
            });
        }

        public async Task<UserDto> GetUserByIdAsync(string userId, string password)
        {
            if (!int.TryParse(userId, out var id)) return new();

            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return new();
            if (!VerifyPassword(password, user.PasswordHash)) return new();


            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                ImageUrl = user.ImageUrl,
                Email = user.Email,
                Roles = [.. user.UserRoles.Select(ur => ur.Role.RoleName)]
            };
        }

        public async Task<bool> DeleteUserAsync(string userId, string password)
        {
            if (!int.TryParse(userId, out var id)) return false;

            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return false;
            if (!VerifyPassword(password, user.PasswordHash)) return false;

            user.IsActive = false;

            _userRepo.Update(user);
            await _userRepo.SaveAsync();
            return true;
        }

        public async Task<(bool, int?, string)> LoginUserAsync(UserLoginDto userLoginDto)
        {
            var user = await _userRepo.GetByUsernameAsync(userLoginDto.Username);
            if (user == null || !VerifyPassword(userLoginDto.Password, user.PasswordHash))
                return (false, null, string.Empty);

            user.LastLogin = DateTime.UtcNow;
            _userRepo.Update(user);
            await _userRepo.SaveAsync();

            return (true, user.UserId, user.ImageUrl);
        }

        public async Task<(bool Success, string Message, int? UserId)> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            if (await _userRepo.GetByUsernameAsync(userRegisterDto.Username) != null)
                return (false, "Username already in use, try another.", null);

            try
            {
                var user = new User
                {
                    Username = userRegisterDto.Username,
                    Email = userRegisterDto.Email,
                    PasswordHash = HashPassword(userRegisterDto.Password),
                    ImageUrl = userRegisterDto.ImageUrl,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                await _userRepo.AddAsync(user);
                await _userRepo.SaveAsync();

                var role = await _roleRepo.GetByNameAsync("User");
                if (role != null)
                {
                    var userRole = new UserRole { UserId = user.UserId, RoleId = role.RoleId };
                    user.UserRoles.Add(userRole);
                    await _userRepo.SaveAsync();
                }

                return (true, "User has been created.", user.UserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error registering user: {Message}", ex.Message);
                return (false, "Unknown error ocurred, please call the system admin.", null);
            }
            
        }

        public async Task<bool> UpdateUserAsync(UserUpdateDto dto)
        {
            var user = await _userRepo.GetByIdAsync(dto.UserId);
            if (user == null) return false;

            if (!string.IsNullOrWhiteSpace(dto.Email))
                user.Email = dto.Email ?? user.Email;

            if (!string.IsNullOrWhiteSpace(dto.ImageUrl))
                user.ImageUrl = dto.ImageUrl ?? user.ImageUrl;

            if (!string.IsNullOrWhiteSpace(dto.NewPassword))
                user.PasswordHash = HashPassword(dto.NewPassword);

            _userRepo.Update(user);
            await _userRepo.SaveAsync();
            return true;
        }

        private static string HashPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = SHA256.HashData(bytes);
            return Convert.ToBase64String(hash);
        }

        private static bool VerifyPassword(string inputPassword, string storedHash)
        {
            var hash = HashPassword(inputPassword);
            return hash == storedHash;
        }
    }
}

﻿using EarthSentry.Contracts.Contracts.Users.Dtos;

namespace EarthSentry.Contracts.Interfaces.Business
{
    public interface IUserBusiness
    {
        Task<(bool Success, string Message, int? UserId)> RegisterUserAsync(UserRegisterDto userRegisterDto);
        Task<(bool Success, int? UserId, string UserImage)> LoginUserAsync(UserLoginDto userLoginDto);
        Task<bool> UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task<bool> DeleteUserAsync(string userId, string password);
        Task<UserDto> GetUserByIdAsync(string userId, string password);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}

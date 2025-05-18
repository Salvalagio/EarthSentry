using EarthSentry.Contracts.Contracts.Users.Dtos;

namespace EarthSentry.Contracts.Interfaces.Business
{
    public interface IUserBusiness
    {
        Task<(bool Success, string Message)> RegisterUserAsync(UserRegisterDto userRegisterDto);
        Task<bool> LoginUserAsync(UserLoginDto userLoginDto);
        Task<bool> UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task<bool> DeleteUserAsync(string userId, string password);
        Task<UserDto> GetUserByIdAsync(string userId, string password);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}

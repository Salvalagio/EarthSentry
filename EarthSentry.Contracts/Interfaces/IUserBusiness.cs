using EarthSentry.Contracts.Users.Dtos;

namespace EarthSentry.Contracts.Interfaces
{
    public interface IUserBusiness
    {
        Task<bool> RegisterUserAsync(UserRegisterDto userRegisterDto);
        Task<bool> LoginUserAsync(UserLoginDto userLoginDto);
        Task<bool> UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task<bool> DeleteUserAsync(string userId);
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
    }
}

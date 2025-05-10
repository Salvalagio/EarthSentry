using EarthSentry.Contracts.Interfaces;
using EarthSentry.Contracts.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthSentry.Domain.Business
{
    public class UserBusiness : IUserBusiness
    {
        public Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }


        public Task<bool> DeleteUserAsync(string userId)
        {
            throw new NotImplementedException();
        }


        public Task<UserDto> GetUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LoginUserAsync(UserLoginDto userLoginDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}

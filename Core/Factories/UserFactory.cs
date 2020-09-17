using Core.Dtos;
using Core.Entities.Identity;

namespace Core.Factories
{
    public class UserFactory
    {
        public UserFactory()
        {
            
        }

        public AppUser Create(string email, string displayName)
        {
            return new AppUser
            {
                Email = email,
                DisplayName = displayName,
                UserName = email
            };
        }

        public UserDto CreateUserDto(AppUser user, string token)
        {
            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = token
            };
        }
    }
}
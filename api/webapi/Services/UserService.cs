using PPM.Interfaces;
using PPM.Models.DTOs;

namespace PPM.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDTO> GetUserDetailsAsync(int user_id)
        {
            var user = await _userRepository.GetUserByIdAsync(user_id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {user_id} was not found.");
            }
            return new UserDTO
            {
                user_id = user.user_id,
                first_name = user.first_name,
                last_name = user.last_name,
                username = user.username,
                is_admin = user.is_admin,
            };
        }
    }
}

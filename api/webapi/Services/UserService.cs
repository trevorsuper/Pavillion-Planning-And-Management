using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PPM.Interfaces;
using PPM.Models;
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
            return new UserDTO
            {
                user_id = user.user_id,
                first_name = user.first_name,
                last_name = user.last_name,
                username = user.username,
                is_admin = user.is_admin,
            };
        }
        /*
        public async Task<UserDTO> GetUserByLoginAsync(LoginUserDTO userDTO)
        {
            var user = await _userRepository.GetUserLoginDetails(userDTO);
            return new UserDTO
            {
                user_id = userDTO.user_id,
                first_name = userDTO.first_name,
                last_name = userDTO.last_name,
                username = userDTO.username,
                is_admin= userDTO.is_admin
            };
            return user;
        }
        */
        public async Task<UserDTO> RegisterUserAsync(RegisterUserDTO userDTO)
        {
            var existing_user = await _userRepository.GetUserByUsernameAsync(userDTO.username);
            if (existing_user != null)
            {
                throw new ApplicationException("Username already exists.");
            }

            var user = new User
            {
                first_name = userDTO.username,
                last_name = userDTO.last_name,
                username = userDTO.username,
                is_admin = false,
            };

            var createdUser = await _userRepository.CreateUserAsync(user);

            return new UserDTO
            {
                user_id = createdUser.user_id,
                first_name = createdUser.first_name,
                last_name = createdUser.last_name,
                username = createdUser.username,
                is_admin=createdUser.is_admin
            };
        }
        public async Task<UserDTO> UpdateUserDetailsAsync(int user_id, UpdateUserDTO userDTO)
        {
            var user = await _userRepository.GetUserByIdAsync(user_id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {user_id} not found.");
            }

            user.first_name = userDTO.first_name;
            user.last_name = userDTO.last_name;
            user.username = userDTO.username;

            await _userRepository.UpdateUserAsync(user);

            return new UserDTO
            {
                user_id = user.user_id,
                first_name=user.first_name,
                last_name=user.last_name,
                username = user.username
            };
        }
        public async Task<UserDTO> UpdateUserAdminDetailsAsync(int user_id, UpdateUserAdminDTO userAdminDTO)
        {
            var user = await _userRepository.GetUserByIdAsync(user_id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {user_id} not found.");
            }

            user.first_name = userAdminDTO.first_name;
            user.last_name =userAdminDTO.last_name;
            user.username = userAdminDTO.username;
            user.is_admin = userAdminDTO.is_admin;

            await _userRepository.UpdateUserAdminAsync(user);

            return new UserDTO
            {
                user_id = user.user_id,
                first_name = user.first_name,
                last_name = user.last_name,
                username = user.username,
                is_admin = user.is_admin
            };
        }
        public async Task DeleteUserRecordById(int user_id)
        {
            var user = await _userRepository.GetUserByIdAsync(user_id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {user_id} not found.");
            }
            await _userRepository.DeleteUserByIdAsync(user_id);
        }
    }
}

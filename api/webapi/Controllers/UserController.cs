using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPM.Models;
using PPM.Models.DTOs;
using PPM.Services;

namespace PPM.Controllers
{
    // Apply authorization and authentication eventually once the project is all finished
    // [Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(UserService userService, ILogger<UserController> logger) : ControllerBase
    {
        private readonly UserService _userService = userService;

        [HttpGet("{user_id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int user_id)
        {
            var user = await _userService.GetUserDetailsAsync(user_id);
            if (user == null)
            {
                logger.LogWarning("User with ID {UserId} not found.", user_id);
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<UserDTO>> CreateUser(RegisterUserDTO userDTO)
        {
            var user = await _userService.RegisterUserAsync(userDTO);
            return Ok(user);
        }

        [HttpPut("UpdateUser/{user_id}")]
        public async Task<IActionResult> UpdateUser(int user_id, UpdateUserDTO userDTO)
        {
            try
            {
               var updatedUser = await _userService.UpdateUserDetailsAsync(user_id, userDTO);
               return Ok(updatedUser);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        //[Authorize(Roles = "Admin")] This line of code will be saved for integration later.
        [HttpPut("UpdateUserAdmin/{user_id}")]
        public async Task<IActionResult> UpdateUserAdmin(int user_id, UpdateUserAdminDTO userAdminDTO)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAdminDetailsAsync(user_id, userAdminDTO);
                if(updatedUser == null)
                {
                    return Unauthorized();
                }
                /*
                if (!updatedUser.is_admin)
                {
                    return Forbid(); //These lines of code work but this will be saved for integration later.
                }
                */
                return Ok(updatedUser);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("DeleteUser/{user_id}")]
        public async Task<IActionResult> DeleteUser(int user_id)
        {
            try
            {
                await _userService.DeleteUserRecordById(user_id);
                return NoContent();
            }
            catch (KeyNotFoundException) 
            {
                return NotFound();
            }
        }
    }
}
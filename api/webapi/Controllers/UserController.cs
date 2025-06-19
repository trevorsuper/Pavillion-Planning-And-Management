using Microsoft.AspNetCore.Mvc;
using PPM.Models.DTOs;
using PPM.Services;

namespace PPM.Controllers
{
    // Apply authorization and authentication eventually once the project is all finished
    // [Authorize] 
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(/*PPMDBContext db,*/ UserService userService, ILogger<UserController> logger) : ControllerBase
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
    }
}
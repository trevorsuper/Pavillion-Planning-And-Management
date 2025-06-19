using Microsoft.AspNetCore.Mvc;
using PPM.Models.DTOs;

namespace PPM.Controllers
{
    // Apply authorization and authentication eventually once the project is all finished
    // [Authorize] 
    //[Route("api/[controller]")]
    //[ApiController]
    public class UserController(PPMDBContext db, ILogger<UserController> logger) : ControllerBase
    {
        [HttpGet("api/GetUser{user_id}")]
        public IActionResult GetUser(int user_id)
        {
            if (user_id <= 0)
            {
                return BadRequest("Invalid User ID.");
            }
            try
            {
                var user_table = db.Users.FirstOrDefault(u => u.user_id == user_id);
                if (user_table == null)
                {
                    return NotFound();
                }
                var user = new UserDTO
                {
                    user_id = user_table.user_id,
                    first_name = user_table.first_name,
                    last_name = user_table.last_name,
                    username = user_table.username,
                    is_admin = user_table.is_admin,
                };
                    return Ok(user);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                logger.LogError(ex, "Error retrieving user with ID {UserId}", user_id);
                return StatusCode(500,"An error occurred while retrieving the user.");
            }
        }
    }
}
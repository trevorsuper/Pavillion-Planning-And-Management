using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPM.Models;
using PPM.Models.DTOs;
using PPM.Services;

namespace PPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkController(ParkService parkService, ILogger<ParkController> logger ) : ControllerBase
    {
        private readonly ParkService _parkService = parkService;
        private readonly ILogger _logger = logger;
        // GET: api/Park/GetAllParks
        [HttpGet("GetAllParks")]
        public async Task<ActionResult<IEnumerable<ParkDTO>>> GetAllParksAsync()
        {
            var parks = await _parkService.GetAllParksInfoAsync();
            return Ok(parks);
        }
        [HttpGet("GetParkById{park_id}")]
        public async Task<ActionResult<ParkDTO>> GetParkByIdAsync(int park_id)
        {
            var park = await _parkService.GetParkByIdAsync(park_id);
            if (park == null)
            {
                logger.LogWarning("Park with name '{ParkName}' not found.", park_id);
                return NotFound($"Park with name '{park_id}' not found.");
            }
            return Ok(park);
        }

        // GET: api/Park/GetParkByName/{park_name}
        [HttpGet("GetParkByName/{park_name}")]
        public async Task<ActionResult<ParkDTO>> GetParkByNameAsync(string park_name)
        {
            var park = await _parkService.GetParkByNameAsync(park_name);
            if (string.IsNullOrWhiteSpace(park_name))
            {
                return BadRequest("Park name cannot be empty.");
            }
            if (park == null)
            {
                logger.LogWarning("Park with name '{ParkName}' not found.", park_name);
                return NotFound($"Park with name '{park_name}' not found.");
            }

            return Ok(park);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPM.Models;

namespace PPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkController(PPMDBContext db, ILogger<ParkController> logger) : ControllerBase
    {
        // GET: api/Park/GetAllParks
        [HttpGet("GetAllParks")]
        public async Task<ActionResult<IEnumerable<Park>>> GetAllParksAsync()
        {
            var parks = await db.Parks.ToListAsync();
            return Ok(parks);
        }

        // GET: api/Park/GetParkByName/{park_name}
        [HttpGet("GetParkByName/{park_name}")]
        public async Task<ActionResult<Park>> GetParkByNameAsync(string park_name)
        {
            var park = await db.Parks
                               .FirstOrDefaultAsync(p => p.ParkName.ToLower() == park_name.ToLower());

            if (park == null)
            {
                logger.LogWarning("Park with name '{ParkName}' not found.", park_name);
                return NotFound($"Park with name '{park_name}' not found.");
            }

            return Ok(park);
        }
    }
}

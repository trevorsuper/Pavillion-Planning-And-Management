using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPM.Models;

namespace PPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkController(PPMDBContext db, ILogger<ParkController> logger) : ControllerBase
    {
        [HttpGet("GetAllParks")]
        public async Task<ActionResult<IEnumerable<Park>>> GetAllParksAsync()
        {
            var parks = await db.Parks.ToListAsync();

            return Ok(parks);
        }
    }
}

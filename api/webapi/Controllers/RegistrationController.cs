using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPM.Models; // Assuming your Registration model is here
using PPM.Data;   // Assuming PPMDBContext is here

namespace PPM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly PPMDBContext _db;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(PPMDBContext db, ILogger<RegistrationController> logger)
        {
            _db = db;
            _logger = logger;
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Registration reg)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            reg.RegistrationDate = DateTime.Now;
            _db.Registrations.Add(reg);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = reg.Id }, reg);
        }

        // READ - All
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regs = await _db.Registrations.ToListAsync();
            return Ok(regs);
        }

        // READ - One
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reg = await _db.Registrations.FindAsync(id);
            if (reg == null) return NotFound();
            return Ok(reg);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Registration updated)
        {
            var reg = await _db.Registrations.FindAsync(id);
            if (reg == null) return NotFound();

            reg.Status = updated.Status;
            reg.EventId = updated.EventId;
            reg.UserId = updated.UserId;

            await _db.SaveChangesAsync();
            return Ok(reg);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var reg = await _db.Registrations.FindAsync(id);
            if (reg == null) return NotFound();

            _db.Registrations.Remove(reg);
            await _db.SaveChangesAsync();
            return Ok("Registration deleted.");
        }
    }
}

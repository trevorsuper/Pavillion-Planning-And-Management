using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPM.Models;

namespace PPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController(PPMDBContext db, ILogger<EventController> logger) : ControllerBase
    {
        // CREATE
        [HttpPost("CreateEvent")]
        public async Task<ActionResult<Event>> CreateEvent(Event newEvent)
        {
            db.Events.Add(newEvent);
            await db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetEventById), new { id = newEvent.Id }, newEvent);
        }

        // READ all
        [HttpGet("GetAllEvents")]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
        {
            var events = await db.Events.ToListAsync();
            return Ok(events);
        }

        // READ one by ID
        [HttpGet("GetEventById/{id}")]
        public async Task<ActionResult<Event>> GetEventById(int id)
        {
            var evt = await db.Events.FindAsync(id);
            if (evt == null)
            {
                logger.LogWarning("Event with ID {EventId} not found.", id);
                return NotFound($"Event with ID {id} not found.");
            }
            return Ok(evt);
        }

        // UPDATE
        [HttpPut("UpdateEvent/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, Event updatedEvent)
        {
            if (id != updatedEvent.Id)
                return BadRequest("Event ID mismatch.");

            var existing = await db.Events.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Title = updatedEvent.Title;
            existing.Description = updatedEvent.Description;
            existing.Date = updatedEvent.Date;
            existing.Capacity = updatedEvent.Capacity;
            existing.ParkId = updatedEvent.ParkId;

            await db.SaveChangesAsync();
            return Ok(existing);
        }

        // DELETE
        [HttpDelete("DeleteEvent/{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var evt = await db.Events.FindAsync(id);
            if (evt == null)
                return NotFound();

            db.Events.Remove(evt);
            await db.SaveChangesAsync();
            return NoContent();
        }
    }
}

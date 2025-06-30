using Microsoft.AspNetCore.Mvc;
using PPM.Models.DTOs;
using PPM.Services;

namespace PPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController(EventRegistrationService eventRegService, ILogger<RegistrationController> logger) : ControllerBase
    {
        private readonly EventRegistrationService _eventRegService = eventRegService;

        [HttpPost("RegisterForEvent")]
        public async Task<ActionResult<EventRegistrationDTO>> RegisterUserForEvent(RegisterEventDTO dto)
        {
            var result = await _eventRegService.RegisterUserForEventAsync(dto);
            if (result == null)
            {
                return BadRequest("User or Event not found, or user is already registered.");
            }
            return Ok(result);
        }

        [HttpGet("GetUserRegistrations/{userId}")]
        public async Task<ActionResult<List<EventRegistrationDTO>>> GetUserRegistrations(int userId)
        {
            var list = await _eventRegService.GetRegistrationsByUserIdAsync(userId);
            return Ok(list);
        }

        [HttpPut("UpdateRegistrationStatus/{registrationId}")]
        public async Task<IActionResult> UpdateStatus(int registrationId, [FromBody] string newStatus)
        {
            var updated = await _eventRegService.UpdateRegistrationStatusAsync(registrationId, newStatus);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("DeleteRegistration/{registrationId}")]
        public async Task<IActionResult> DeleteRegistration(int registrationId)
        {
            var success = await _eventRegService.DeleteRegistrationAsync(registrationId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}

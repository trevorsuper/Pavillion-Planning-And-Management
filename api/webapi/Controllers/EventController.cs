using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PPM.Models;
using PPM.Models.DTOs;
using PPM.Models.Services;

namespace PPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;
        private readonly ILogger<EventController> _logger;

        public EventController(EventService eventService, ILogger<EventController> logger)
        {
            _eventService = eventService;
            _logger = logger;
        }

        // POST /api/Event
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EventDTO dto)
        {
            var ev = new Event
            {
                event_name = dto.event_name,
                event_description = dto.event_description,
                event_start_date = dto.event_start_date.Date,
                event_end_date = dto.event_end_date.Date,
                event_start_time = dto.event_start_time,
                event_end_time = dto.event_end_time
            };

            await _eventService.CreateAsync(ev);
            return Ok(new { ev.event_id });
        }
    }
}


﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PPM.Models;
using PPM.Models.DTOs;
using PPM.Models.Services;
using PPM.Services;

namespace PPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly RegistrationService _registrationService;
        private readonly UserService _userService;
        private readonly ParkService _parkService;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(
            RegistrationService registrationService,
            UserService userService,
            ParkService parkService,
            ILogger<RegistrationController> logger)
        {
            _registrationService = registrationService;
            _userService = userService;
            _parkService = parkService;
            _logger = logger;
        }

        // POST /api/Registration
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO dto)
        {
            _logger.LogInformation("Starting registration for User ID: {UserId} and Park ID: {ParkId}", dto.user_id, dto.park_id);

            var user = await _userService.GetUserByIdAsync(dto.user_id);
            var park = await _parkService.GetParkEntityByIdAsync(dto.park_id);

            if (user == null || park == null)
            {
                _logger.LogWarning("User or Park not found. User ID: {UserId}, Park ID: {ParkId}", dto.user_id, dto.park_id);
                return BadRequest("User or Park not found.");
            }

            var registration = new Registration
            {
                user_id = dto.user_id,
                park_id = dto.park_id,
                requested_park = dto.requested_park,  
                pavillion = dto.pavillion,
                start_time = dto.start_time,
                end_time = dto.end_time,
                is_approved = false,
                registration_date = dto.registration_date,
                User = user,
                Park = park
            };

            await _registrationService.CreateAsync(registration);
            _logger.LogInformation("Registration created with ID: {RegistrationId}", registration.registration_id);

            return Ok(new
            {
                RegistrationId = registration.registration_id
            });
        }

        // GET /api/Registration/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBookingsForUser(int userId)
        {
            var bookings = await _registrationService.GetBookingsByUserIdAsync(userId);

            if (bookings == null || !bookings.Any())
            {
                return NotFound($"No bookings found for User ID {userId}.");
            }

            return Ok(bookings);
        }
    }
}


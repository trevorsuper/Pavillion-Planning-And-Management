using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPM.Models;

namespace PPM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController(ILogger<EventController> logger) : ControllerBase
    {
       
    }
}

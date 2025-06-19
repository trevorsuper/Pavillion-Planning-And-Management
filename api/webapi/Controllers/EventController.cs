using Microsoft.AspNetCore.Mvc;
using PPM;
using PPM.Controllers;

namespace PPM.Controllers
{
    public class EventController(PPMDBContext db, ILogger<ParkController> logger) : ControllerBase
    {
    }
}

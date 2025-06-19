using Microsoft.AspNetCore.Mvc;
using PPM;
using PPM.Controllers;

namespace PPM.Controllers
{
    public class RegistrationController(PPMDBContext db, ILogger<ParkController> logger) : ControllerBase
    {
    }
}

using Microsoft.AspNetCore.Mvc;
using PPM.Models;

namespace PPM.Controllers
{
    public class UserController(PPMDBContext db) : ControllerBase
    {
        [HttpGet("api/GetUser")]
        public User GetUser()
        {
            var user = db.Users.First();
            return user;
        }
    }
}
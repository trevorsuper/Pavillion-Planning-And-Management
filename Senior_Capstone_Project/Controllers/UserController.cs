using Microsoft.AspNetCore.Mvc;
using Senior_Capstone_Project.Models;

namespace Senior_Capstone_Project.Controllers
{
    public class  UserController(SeniorCapstoneProjectDBContext db) : ControllerBase{
        [HttpGet("api/GetUser")]
        public User GetUser() {
            var user = db.Users.First();
            return user;
        }
    }
}

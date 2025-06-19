using Microsoft.EntityFrameworkCore;
using PPM.Interfaces;
using PPM.Models;

namespace PPM.Repositories
{
    public class ParkRepository : IParkRepository
    {
        private readonly PPMDBContext _db;
        public ParkRepository(PPMDBContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Park>> GetAllParksAsync()
        {
            var parks = await _db.Parks.ToListAsync();
            return parks;
        }
    }
}

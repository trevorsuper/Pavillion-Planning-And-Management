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
        public async Task<Park> GetParkByIdAsync(int park_id)
        {
            var park = await _db.Parks.FirstOrDefaultAsync(p => p.park_id == park_id);
            return park!; //Put the ! there to tell the program that this will not be null
        }
        public async Task<Park> GetParkByNameAsync(string park_name)
        {
            var park = await _db.Parks.FirstOrDefaultAsync(p => p.park_name.ToLower() == park_name.Trim().ToLowerInvariant());
            return park!; //Put the ! there to tell the program that this will not be null
        }
        public async Task<IEnumerable<Park>> GetAllParksAsync()
        {
            var parks = await _db.Parks.ToListAsync();
            return parks;
        }
    }
}

using PPM.Models;
using PPM.Models.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPM.Models.Services
{
    public class RegistrationService
    {
        private readonly IRegistrationRepository _repo;

        public RegistrationService(IRegistrationRepository repo)
        {
            _repo = repo;
        }

        public Task CreateAsync(Registration r)
        {
            return _repo.AddAsync(r);
        }

        public Task UpdateAsync(Registration r)
        {
            return _repo.UpdateAsync(r);
        }

        public Task<IEnumerable<Registration>> GetBookingsByUserIdAsync(int userId)
        {
            return _repo.GetByUserIdAsync(userId);
        }
    }
}

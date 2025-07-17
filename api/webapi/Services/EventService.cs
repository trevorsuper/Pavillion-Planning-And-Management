// EventService.cs
using PPM.Models.Interfaces;

namespace PPM.Models.Services
{
    public class EventService
    {
        private readonly IEventRepository _repo;
        public EventService(IEventRepository repo) => _repo = repo;
        public Task CreateAsync(Event ev) => _repo.AddAsync(ev);
    }
}

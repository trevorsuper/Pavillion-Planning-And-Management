// EventRepository.cs
using PPM;
using PPM.Models.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PPM.Models.Services
{
    public class EventRepository : IEventRepository
    {
        private readonly PPMDBContext _ctx;
        public EventRepository(PPMDBContext ctx) => _ctx = ctx;
        public async Task AddAsync(Event ev)
        {
            _ctx.Event.Add(ev);
            await _ctx.SaveChangesAsync();
        }
    }
}

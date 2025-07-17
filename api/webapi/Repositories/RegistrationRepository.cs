﻿using PPM;
using PPM.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PPM.Models.Services
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly PPMDBContext _ctx;

        public RegistrationRepository(PPMDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(Registration registration)
        {
            _ctx.Registration.Add(registration);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(Registration registration)
        {
            _ctx.Registration.Update(registration);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Registration>> GetByUserIdAsync(int userId)
        {
            return await _ctx.Registration
                .Where(r => r.user_id == userId)
                .ToListAsync();
        }
    }
}

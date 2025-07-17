﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPM.Models.Interfaces
{
    public interface IRegistrationRepository
    {
        Task AddAsync(Registration registration);
        Task UpdateAsync(Registration registration);

        Task<IEnumerable<Registration>> GetByUserIdAsync(int userId);
    }
}


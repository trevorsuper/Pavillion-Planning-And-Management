using Microsoft.AspNetCore.Mvc;
using PPM.Models;

namespace PPM.Interfaces
{
    public interface IParkRepository
    {
        Task<IEnumerable<Park>> GetAllParksAsync();
    }
}

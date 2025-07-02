using Microsoft.AspNetCore.Mvc;
using PPM.Models;

namespace PPM.Interfaces
{
    public interface IParkRepository
    {
        Task<Park> GetParkByIdAsync(int park_id);
        Task<Park> GetParkByNameAsync(string park_name);
        Task<IEnumerable<Park>> GetAllParksAsync();
    }
}

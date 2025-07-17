// IEventRepository.cs
namespace PPM.Models.Interfaces
{
    public interface IEventRepository
    {
        Task AddAsync(Event ev);
    }
}

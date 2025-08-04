namespace PPM.Models.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<Registration> CreateRegistrationAsync(Registration registration);
        Task<Registration> GetRegistrationByIdAsync(int registration_id);
        Task<IEnumerable<Registration>> GetAllUserRegistrationsAsync(int user_id);
        Task<IEnumerable<Registration>> GetAllAdminRegistrationsAsync();
        Task<IEnumerable<Registration>> GetAllUnreviewedRegistrationsAsync();
        Task<IEnumerable<Registration>> GetAllUnapprovedRegistrationsAsync();
        Task<IEnumerable<Registration>> GetAllApprovedRegistrationsAsync();
        Task<Registration> UpdateRegistrationAsync(Registration registration);
    }
}

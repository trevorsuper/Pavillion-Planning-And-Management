using PPM.Models;

namespace PPM.Interfaces
{
    //“A contract that any class that implements this must have a method called GetUserByIdAsync(int) that returns a Task<User>.”
    //It's called a contract because it guarantees that anything claiming to be an IUserRepository will behave in a known way
    //That being it will have that method, and return that type of result. So when the code uses the interface, it doesn't care
    //how it's implemented, just that it works accordingly to the contract.
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int user_id);
    }
}

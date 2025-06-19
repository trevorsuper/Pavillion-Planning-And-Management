using PPM.Interfaces;
using PPM.Models;

namespace PPM.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PPMDBContext _db;
        public UserRepository(PPMDBContext db)
        {
            _db = db;
        }
        public async Task<User> GetUserByIdAsync(int user_id)
        {
            //Asynchronously searches the Users table for an entity with the primary key value user_id through in-memory change tracker first.
            //If the entity is already loaded/tracked in the current DbContext, it returns that instance immediately without querying the database.
            //Otherwise, it executes an SQL Query to retrieve it from the database.
            var user = await _db.Users.FindAsync(user_id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {user_id} was not found.");
            }
            return user;
        }
    }
}

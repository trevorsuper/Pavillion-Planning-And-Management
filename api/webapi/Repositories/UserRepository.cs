using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPM.Interfaces;
using PPM.Models;
using System.Net.Mail;

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
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            if (!IsValidUsername(username))
            {
                throw new KeyNotFoundException("Username Invalid.");
            }
            string username_normalized = username.ToLowerInvariant().Trim(); // Normalizes the username once it hits the database
            return await _db.Users.FirstOrDefaultAsync(u => u.username == username_normalized);
        }
        public async Task<User> CreateUserAsync(User user)
        {
            user.username = user.username.ToLowerInvariant().Trim();
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateUserAsync(User user)
        {
            var existing_user = await _db.Users.FindAsync(user.user_id);
            if (existing_user == null)
            {
                throw new KeyNotFoundException("User Does Not Exist.");
            }
            if (!IsValidUsername(user.username))
            {
                throw new KeyNotFoundException("Username Invalid.");
            }
            existing_user.username = user.username.ToLowerInvariant().Trim();
            existing_user.first_name = user.first_name;
            existing_user.last_name = user.last_name;
            await _db.SaveChangesAsync();
            return existing_user;
        }
        public async Task<User> UpdateUserAdminAsync(User user)
        {
            var existing_user = await _db.Users.FindAsync(user.user_id);
            if (existing_user == null)
            {
                throw new KeyNotFoundException("User Does Not Exist.");
            }
            if (!IsValidUsername(user.username))
            {
                throw new KeyNotFoundException("Username Invalid.");
            }
            existing_user.username = user.username.ToLowerInvariant().Trim();
            existing_user.first_name = user.first_name;
            existing_user.last_name = user.last_name;
            await _db.SaveChangesAsync();
            return existing_user;
        }
        public async Task DeleteUserByIdAsync(int user_id)
        {
            var user = await _db.Users.FindAsync(user_id); //Directly looks up the primary key (user_id) which is exactly what I want to delete.
            if (user != null)
            {
               _db.Users.Remove(user);
               await _db.SaveChangesAsync();
            }
        }
        public bool IsValidUsername(string username)
        {
            return !string.IsNullOrEmpty(username) 
                && username.Trim().Length == username.Length //Removes trailing spaces
                && !username.Contains(' ')
                && username.All(char.IsLetterOrDigit); //The Last Field enforces Alphanumeric Character support
        }
    }
}
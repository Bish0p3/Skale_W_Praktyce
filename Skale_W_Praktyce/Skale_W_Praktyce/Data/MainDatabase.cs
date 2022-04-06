using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Skale_W_Praktyce.Models;

namespace Skale_W_Praktyce.Data
{
    public class MainDatabase
    {
        readonly SQLiteAsyncConnection database;

        public MainDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<User>().Wait();
            database.CreateTableAsync<Bookmark>().Wait();
        }
        public Task<List<User>> GetUsersAsync()
        {
            //Get all users.
            return database.Table<User>().ToListAsync();
        }

        public Task<List<Bookmark>> GetBookmarksAsync()
        {
            //Get all bookamrk.
            return database.Table<Bookmark>().ToListAsync();
        }
        public Task<User> GetUserAsyncID(int id)
        {
            // Get a specific bookamrk.
            return database.Table<User>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<Bookmark> GetBookmarkAsyncID(int id)
        {
            // Get a specific bookamrk.
            return database.Table<Bookmark>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }
        public Task<User> GetUserAsyncName(string name)
        {
            // Get a specific bookamrk.
            return database.Table<User>()
                            .Where(i => i.Username == name)
                            .FirstOrDefaultAsync();
        }

        public Task<Bookmark> GetBookmarkAsyncName(string name)
        {
            // Get a specific bookamrk.
            return database.Table<Bookmark>()
                            .Where(i => i.ScaleName == name)
                            .FirstOrDefaultAsync();
        }
        public Task<int> SaveUserAsync(User user)
        {
            if (user.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(user);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(user);
            }
        }

        public Task<int> SaveBookmarkAsync(Bookmark bookmark)
        {
            if (bookmark.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(bookmark);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(bookmark);
            }
        }
        public Task<int> DeleteUserAsync(User user)
        {
            // Delete a note.
            return database.DeleteAsync(user);
        }

        public Task<int> DeleteBookmarkAsync(Bookmark bookmark)
        {
            // Delete a note.
            return database.DeleteAsync(bookmark);
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Skale_W_Praktyce.Models;

namespace Skale_W_Praktyce.Data
{
    public class BookmarkDatabase
    {
        readonly SQLiteAsyncConnection database;

        public BookmarkDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Bookmark>().Wait();
        }

        public Task<List<Bookmark>> GetBookmarksAsync()
        {
            //Get all bookamrk.
            return database.Table<Bookmark>().ToListAsync();
        }

        public Task<Bookmark> GetBookmarkAsyncID(int id)
        {
            // Get a specific bookamrk.
            return database.Table<Bookmark>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }
        public Task<Bookmark> GetBookmarkAsyncName(string name)
        {
            // Get a specific bookamrk.
            return database.Table<Bookmark>()
                            .Where(i => i.ScaleName == name)
                            .FirstOrDefaultAsync();
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

        public Task<int> DeleteBookmarkAsync(Bookmark bookmark)
        {
            // Delete a note.
            return database.DeleteAsync(bookmark);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MVMMLogin.Models;
using SQLite;
using MVMMLogin.Helpers;

namespace MVMMLogin
{
    public class Database
    {

        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            //Establishing the conection
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Contact>().Wait();
            _database.CreateTableAsync<User>().Wait();
        }

        // Create an User
        public Task<int> SaveUserAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        // Retrieve an User
        public Task<User> GetUserAsync(User user)
        {
            return _database.Table<User>().FirstOrDefaultAsync();
        }

        public Task<User> GetUserAsync(string username, string password)
        {
            var user = _database.Table<User>().Where(x => x.Username == username || x.Password == password).FirstOrDefaultAsync();
            return user;
        }

        public async Task<bool> LoginUserAsync(string username, string password)
        {
            var usersTable = _database.Table<User>();
            var correspondingUser = await usersTable.Where(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();
            if (correspondingUser != null)
            {
                return true;
            }
            else
                return false;
        }


        // Show the registers
        public async Task<ObservableCollection<Contact>> GetContactsAsync()
        {
            var contactsList = await _database.Table<Contact>().ToListAsync();
            return contactsList.ToObservableCollection();
        }

        public async Task<ObservableCollection<Contact>>  GetContactsForUserAsync(string email)
        {
            var user = await _database.Table<Contact>().Where(x => x.ParentUserEmail == email).ToListAsync();
            return user.ToObservableCollection();
        }

        // Save registers
        public Task<int> SaveContactAsync(Contact contact)
        {
            return _database.InsertAsync(contact);
        }

        // Delete registers
        public Task<int> DeleteContactAsync(Contact contact)
        {
            return _database.DeleteAsync(contact);
        }

        // Save registers
        public Task<int> UpdateContactAsync(Contact contact)
        {
            return _database.UpdateAsync(contact);
        }
    }
}

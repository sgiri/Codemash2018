using CodeMash.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeMash.Data
{
    public class CodeMashDatabase
    {
        readonly SQLiteAsyncConnection database;

        public CodeMashDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<FavSessionModel>().Wait();
           
        }

        public Task<List<FavSessionModel>> GetItemsAsync()
        {
            return database.Table<FavSessionModel>().ToListAsync();
        }

        public Task<int> SaveItems(List<FavSessionModel> items)
        {
            try
            {
                database.DropTableAsync<FavSessionModel>().Wait();
                database.CreateTableAsync<FavSessionModel>().Wait();
                return database.InsertAllAsync(items);
            }
            catch(Exception ex)
            {
                string jj = ex.Message;
            }
            return null;
        }

        public Task<List<FavSessionModel>> GetItems()
        {
            return database.QueryAsync<FavSessionModel>("SELECT * FROM [FavSessionModel]");
        }

        //public Task<SessionModel> GetItemAsync(int id)
        //{
        //    return database.Table<SessionModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        //}

        //public Task<int> SaveItemAsync(SessionModel item)
        //{
        //    if (item.Id != 0)
        //    {
        //        return database.UpdateAsync(item);
        //    }
        //    else
        //    {
        //        return database.InsertAsync(item);
        //    }
        //}

        //public Task<int> DeleteItemAsync(FavSessionModel item)
        //{
        //    return database.DeleteAsync(item);
        //}
    }
}

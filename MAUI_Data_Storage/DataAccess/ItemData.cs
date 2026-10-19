using SQLite;
using MAUI_Data_Storage.Models;

namespace MAUI_Data_Storage.DataAccess
{
    // Data Access Layer for the Items table
    public class ItemData
    {
        SQLiteAsyncConnection? database;

        async Task Init()
        {
            if (database is not null)
                return;

            database = new SQLiteAsyncConnection(DatabaseConstants.DatabasePath, DatabaseConstants.Flags);
            await database.CreateTableAsync<Item>();
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            await Init();
            return await database!.Table<Item>().OrderBy(i => i.ItemId).ToListAsync();
        }

        public async Task<Item?> GetItemByItemIdAsync(string itemId)
        {
            await Init();
            return await database!.Table<Item>().Where(i => i.ItemId == itemId).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Item item)
        {
            await Init();
            return item.Id != 0
                ? await database!.UpdateAsync(item)
                : await database!.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(Item item)
        {
            await Init();
            return await database!.DeleteAsync(item);
        }
    }
}

using System.Collections.Generic;
using System.Data.SQLite;
using InterviewTask.ListOfItems.WebApi.Models;

namespace InterviewTask.ListOfItems.WebApi.DAL
{
    public interface IItemsRepository
    {
        void DeleteFromDatabase(SQLiteConnection connection, string id);
        IEnumerable<Item> GetDataFromDatabase(SQLiteConnection connection);
        void InsertToDatabase(SQLiteConnection connection, string table, string columns, string values);
        IEnumerable<Item> QueryDatabase(SQLiteConnection connection, string query);
        void UpdateDatabase(SQLiteConnection connection, Item item);
        bool TableExists(string tableName, SQLiteConnection connection);
    }
}
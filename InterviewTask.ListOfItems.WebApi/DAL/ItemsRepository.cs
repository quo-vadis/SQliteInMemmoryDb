using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using InterviewTask.ListOfItems.WebApi.Models;

namespace InterviewTask.ListOfItems.WebApi.DAL
{
    public class ItemsRepository : IItemsRepository
    {

        public IEnumerable<Item> GetDataFromDatabase(SQLiteConnection connection)
        {
            SQLiteCommand com = new SQLiteCommand("SELECT COUNT(*) from Items", connection);

            IEnumerable<Item> itemsList = new List<Item>();
            int result = int.Parse(com.ExecuteScalar().ToString());

            //if database isn't empty
            if (result != 0)
            {
                itemsList = new List<Item>(QueryDatabase(connection,
                    "SELECT * FROM Items"));
                ;
            }
            return itemsList;
        }

        public bool TableExists(string tableName, SQLiteConnection connection)
        {
            SQLiteCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM sqlite_master WHERE type = 'table' AND name = @name";
            cmd.Parameters.Add("@name", DbType.String).Value = tableName;
            return (cmd.ExecuteScalar() != null);
        }


        public void InsertToDatabase(SQLiteConnection connection, string table, string columns, string values)
        {
            try
            {
                string insertionString = "INSERT INTO " + table + " (" + columns + ") VALUES (" + values + ")";
                SQLiteCommand insertionCommand = new SQLiteCommand(insertionString, connection);
                insertionCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

        }

        //Get data from table
        public IEnumerable<Item> QueryDatabase(SQLiteConnection connection, string query)
        {
            List<Item> queryResult = new List<Item>();

            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.Open();
                }

                SQLiteCommand queryCommand = new SQLiteCommand(query, connection);
                SQLiteDataReader queryReader = queryCommand.ExecuteReader();

                while (queryReader.Read())
                {
                    var item = new Item
                    {
                        Id = Convert.ToInt32(queryReader["Id"]),
                        Name = queryReader["Name"].ToString(),
                        Type = queryReader["Type"].ToString(),
                    };
                    queryResult.Add(item);
                }

                if (queryResult.Any())
                {
                    return queryResult;
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Updates an excisting database record.
        public void UpdateDatabase(SQLiteConnection connection, Item item)
        {
            try
            {
                string updateString = $"UPDATE Items SET Name ='{item.Name}', Type = '{item.Type}' Where Id = {item.Id}";
                SQLiteCommand updateCommand = new SQLiteCommand(updateString, connection);
                updateCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // Deletes row from database.
        public void DeleteFromDatabase(SQLiteConnection connection, string Id)
        {
            try
            {
                string deletionString = "DELETE FROM Items WHERE Id =" + Id;
                SQLiteCommand deletionCommand = new SQLiteCommand(deletionString, connection);
                deletionCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
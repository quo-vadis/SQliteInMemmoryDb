using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using InterviewTask.ListOfItems.WebApi.DAL;
using InterviewTask.ListOfItems.WebApi.Models;

namespace InterviewTask.ListOfItems.WebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ItemsController : ApiController
    {
        static SQLiteConnection _connection;
        private IItemsRepository _repo;
        private IEnumerable<Item> _itemsList;

        public ItemsController()
        {
            _connection = new SQLiteConnection("FullUri=file::memory:?cache=shared");
            _repo = new ItemsRepository();
            using (SQLiteConnection connection = new SQLiteConnection(_connection))
            {
                connection.Open();
                if (!_repo.TableExists("Items", connection))
                {
                    string sql =
                        "CREATE TABLE IF NOT EXISTS Items(Id INTEGER PRIMARY KEY,name varchar(50), type varchar(50))";
                    SQLiteCommand command = new SQLiteCommand(sql, connection);
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Item> Get()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connection))
            {
                connection.Open();

                _itemsList = _repo.GetDataFromDatabase(connection) ?? null;
            }

            return _itemsList;
        }

        // POST: api/Items
        [HttpPost]
        public IEnumerable<Item> Post([FromBody] Item item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connection))
            {
                connection.Open();
                _repo.InsertToDatabase(connection, "Items", "Name,Type", $"'{item.Name}','{item.Type}'");

                _itemsList = _repo.GetDataFromDatabase(connection);
            }

            return _itemsList;
        }

        //PUT: api/Items/5
        [HttpPut]
        public IEnumerable<Item> Put(int id, [FromBody] Item newItem)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connection))
            {
                connection.Open();
                _repo.UpdateDatabase(connection, newItem);

                _itemsList = _repo.GetDataFromDatabase(connection);
            }

            return _itemsList;
        }

        // DELETE: api/Items/5
        [HttpDelete]
        public IEnumerable<Item> Delete(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connection))
            {
                connection.Open();
                _repo.DeleteFromDatabase(connection, id.ToString());

                _itemsList = _repo.GetDataFromDatabase(connection);
            }

            return _itemsList;
        }
        
    }
}

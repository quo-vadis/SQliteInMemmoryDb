using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListOfItems.DAL
{
    public class Class1
    {
        public void f()
        {
            using (SQLiteConnection connection = new SQLiteConnection(
                "Data Source=:memory:;"))
            {
                connection.Open();

                connection.CreateModule(new SQLiteModuleEnumerable(
                    "sampleModule", new string[] { "one", "two", "three" }));

                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText =
                        "CREATE VIRTUAL TABLE t1 USING sampleModule;";

                    command.ExecuteNonQuery();
                }

                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM t1;";

                    using (SQLiteDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                            Console.WriteLine(dataReader[0].ToString());
                    }
                }

                connection.Close();
            }

        }
    }
}
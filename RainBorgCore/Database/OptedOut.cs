using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace RainBorg
{
    class OptedOut
    {
        internal static void Load()
        {
            using (SqliteConnection Connection = new SqliteConnection("Data Source=" + RainBorg.databaseFile))
            {
                Connection.Open();
                SqliteCommand Command = new SqliteCommand(@"
                    CREATE TABLE IF NOT EXISTS optedout (
                        id INTEGER UNIQUE
                    )
                ", Connection);
                Command.ExecuteNonQuery();
            }
        }

        internal static bool ContainsKey(ulong Id)
        {
            using (SqliteConnection Connection = new SqliteConnection("Data Source=" + RainBorg.databaseFile))
            {
                Connection.Open();
                SqliteCommand Command = new SqliteCommand("SELECT id FROM optedout WHERE id = @id", Connection);
                Command.Parameters.AddWithValue("id", Id);
                using (SqliteDataReader Reader = Command.ExecuteReader())
                    if (Reader.Read()) return true;
                    else return false;
            }
        }

        internal static void Add(ulong Id)
        {
            using (SqliteConnection Connection = new SqliteConnection("Data Source=" + RainBorg.databaseFile))
            {
                Connection.Open();
                SqliteCommand Command = new SqliteCommand("INSERT OR REPLACE INTO optedout (id) VALUES (@id)", Connection);
                Command.Parameters.AddWithValue("id", Id);
                Command.ExecuteNonQuery();
            }
        }

        internal static void Remove(ulong Id)
        {
            using (SqliteConnection Connection = new SqliteConnection("Data Source=" + RainBorg.databaseFile))
            {
                Connection.Open();
                SqliteCommand Command = new SqliteCommand("DELETE FROM optedout WHERE id = @id", Connection);
                Command.Parameters.AddWithValue("id", Id);
                Command.ExecuteNonQuery();
            }
        }

        internal static List<ulong> ToList()
        {
            using (SqliteConnection Connection = new SqliteConnection("Data Source=" + RainBorg.databaseFile))
            {
                Connection.Open();
                List<ulong> Output = new List<ulong>();
                SqliteCommand Command = new SqliteCommand("SELECT id FROM optedout", Connection);
                using (SqliteDataReader Reader = Command.ExecuteReader())
                    while (Reader.Read())
                        Output.Add((ulong)Reader.GetInt64(0));
                return Output;
            }
        }

        internal static int Count
        {
            get { return ToList().Count; }
        }
    }
}

using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace RainBorg
{
    class Blacklist
    {
        internal static void Load()
        {
            using (SqliteConnection Connection = new SqliteConnection("Data Source=" + RainBorg.databaseFile))
            {
                Connection.Open();
                SqliteCommand Command = new SqliteCommand(@"
                    CREATE TABLE IF NOT EXISTS blacklist (
                        id INTEGER UNIQUE,
                        reason TEXT DEFAULT ''
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
                SqliteCommand Command = new SqliteCommand("SELECT id FROM blacklist WHERE id = @id", Connection);
                Command.Parameters.AddWithValue("id", Id);
                using (SqliteDataReader Reader = Command.ExecuteReader())
                    if (Reader.Read()) return true;
                    else return false;
            }
        }

        internal static void Add(ulong Id, string Reason)
        {
            using (SqliteConnection Connection = new SqliteConnection("Data Source=" + RainBorg.databaseFile))
            {
                Connection.Open();
                SqliteCommand Command = new SqliteCommand("INSERT OR REPLACE INTO blacklist (id, reason) VALUES (@id, @reason)", Connection);
                Command.Parameters.AddWithValue("id", Id);
                Command.Parameters.AddWithValue("reason", Reason);
                Command.ExecuteNonQuery();
            }
        }

        internal static void Remove(ulong Id)
        {
            using (SqliteConnection Connection = new SqliteConnection("Data Source=" + RainBorg.databaseFile))
            {
                Connection.Open();
                SqliteCommand Command = new SqliteCommand("DELETE FROM blacklist WHERE id = @id", Connection);
                Command.Parameters.AddWithValue("id", Id);
                Command.ExecuteNonQuery();
            }
        }

        internal static Dictionary<ulong, string> ToList()
        {
            using (SqliteConnection Connection = new SqliteConnection("Data Source=" + RainBorg.databaseFile))
            {
                Connection.Open();
                Dictionary<ulong, string> Output = new Dictionary<ulong, string>();
                SqliteCommand Command = new SqliteCommand("SELECT id, reason FROM blacklist", Connection);
                using (SqliteDataReader Reader = Command.ExecuteReader())
                    while (Reader.Read())
                        Output.Add((ulong)Reader.GetInt64(0), Reader.GetString(1));
                return Output;
            }
        }

        internal static int Count
        {
            get { return ToList().Count; }
        }
    }
}

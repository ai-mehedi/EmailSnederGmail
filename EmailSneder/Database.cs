using System;
using System.Data.SQLite;
using System.IO;

namespace EmailSender
{
    public class MyDatabase
    {
        private string dbPath;
        private SQLiteConnection connection;

        public MyDatabase()
       : this("database.sqlite") 
        {
        }
        public MyDatabase(string dbFileName = "database.sqlite") 
        {
            dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, dbFileName);

            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                Console.WriteLine("Database file created.");
            }

            connection = new SQLiteConnection($"Data Source={dbPath};Version=3;");
            connection.Open();
            Console.WriteLine("Connected to SQLite database.");

            CreateTables();
        }

        private void CreateTables()
        {
            string query = @"
                CREATE TABLE IF NOT EXISTS users (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    email TEXT UNIQUE NOT NULL,
                    password TEXT NOT NULL
                );

                CREATE TABLE IF NOT EXISTS smtp (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    email TEXT UNIQUE NOT NULL,
                    password TEXT NOT NULL,
                    status TEXT CHECK(status IN ('active', 'inactive')) NOT NULL DEFAULT 'inactive'
                );

                CREATE TABLE IF NOT EXISTS message (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    subject TEXT NOT NULL,
                    body TEXT NOT NULL,
                    status TEXT CHECK(status IN ('pending', 'sent', 'failed')) NOT NULL DEFAULT 'pending'
                );

                CREATE TABLE IF NOT EXISTS email (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    firstname TEXT NOT NULL,
                    lastname TEXT NOT NULL,
                    email TEXT UNIQUE NOT NULL,
                    status TEXT CHECK(status IN ('sent', 'fail')) NOT NULL DEFAULT 'fail'
                );
            ";

            using (var cmd = new SQLiteCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Tables created or ensured.");
            }
        }


        public bool Login(string email, string password)
        {
            string query = "SELECT COUNT(*) FROM users WHERE email = @email AND password = @password";

            using (var cmd = new SQLiteCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password); // plain-text, improve with hashing later

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                return count > 0; // If user exists, return true
            }
        }

        // INSERT Operations (Create)
        public void InsertUser(string email, string password)
        {
            ExecuteNonQuery("INSERT INTO users (email, password) VALUES (@email, @password)", ("@email", email), ("@password", password));
        }

        public void InsertSMTP(string email, string password, string status)
        {
            ExecuteNonQuery("INSERT INTO smtp (email, password, status) VALUES (@email, @password, @status)", ("@email", email), ("@password", password), ("@status", status));
        }

        public void InsertMessage(string subject, string body, string status = "pending")
        {
            ExecuteNonQuery("INSERT INTO message (subject, body, status) VALUES (@subject, @body, @status)", ("@subject", subject), ("@body", body), ("@status", status));
        }

        public void InsertEmail(string firstname, string lastname, string email, string status = "fail")
        {
            ExecuteNonQuery("INSERT INTO email (firstname, lastname, email, status) VALUES (@firstname, @lastname, @email, @status)", ("@firstname", firstname), ("@lastname", lastname), ("@email", email), ("@status", status));
        }

        // READ Operations (Retrieve Data)
        public void GetUsers()
        {
            ExecuteReader("SELECT * FROM users");
        }

        public void GetSMTPs()
        {
            ExecuteReader("SELECT * FROM smtp");
        }

        public void GetMessages()
        {
            ExecuteReader("SELECT * FROM message");
        }

        public void GetEmails()
        {
            ExecuteReader("SELECT * FROM email");
        }

        // UPDATE Operations (Update All Fields)
        public void UpdateUser(int id, string email, string password)
        {
            ExecuteNonQuery("UPDATE users SET email = @email, password = @password WHERE id = @id", ("@id", id.ToString()), ("@email", email), ("@password", password));
        }

        public void UpdateSMTP(int id, string email, string password, string status)
        {
            ExecuteNonQuery("UPDATE smtp SET email = @email, password = @password, status = @status WHERE id = @id", ("@id", id.ToString()), ("@email", email), ("@password", password), ("@status", status));
        }

        public void UpdateMessage(int id, string subject, string body, string status)
        {
            ExecuteNonQuery("UPDATE message SET subject = @subject, body = @body, status = @status WHERE id = @id", ("@id", id.ToString()), ("@subject", subject), ("@body", body), ("@status", status));
        }
   

        public void UpdateEmail(int id, string firstname, string lastname, string email, string status)
        {
            ExecuteNonQuery("UPDATE email SET firstname = @firstname, lastname = @lastname, email = @email, status = @status WHERE id = @id", ("@id", id.ToString()), ("@firstname", firstname), ("@lastname", lastname), ("@email", email), ("@status", status));
        }

        // DELETE Operations (Remove Data)
        public void DeleteUser(int id)
        {
            ExecuteNonQuery("DELETE FROM users WHERE id = @id", ("@id", id.ToString()));
        }

        public void DeleteSMTP(int id)
        {
            ExecuteNonQuery("DELETE FROM smtp WHERE id = @id", ("@id", id.ToString()));
        }

        public void DeleteMessage(int id)
        {
            ExecuteNonQuery("DELETE FROM message WHERE id = @id", ("@id", id.ToString()));
        }

        public void DeleteEmail(int id)
        {
            ExecuteNonQuery("DELETE FROM email WHERE id = @id", ("@id", id.ToString()));
        }

        // Helper Methods
        private void ExecuteNonQuery(string query, params (string, string)[] parameters)
        {
            using (var cmd = new SQLiteCommand(query, connection))
            {
                foreach (var param in parameters)
                    cmd.Parameters.AddWithValue(param.Item1, param.Item2);

                cmd.ExecuteNonQuery();
                Console.WriteLine("Operation successful.");
            }
        }

        private void ExecuteReader(string query)
        {
            using (var cmd = new SQLiteCommand(query, connection))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        Console.Write($"{reader.GetName(i)}: {reader[i]} | ");

                    Console.WriteLine();
                }
            }
        }

        public void CloseConnection()
        {
            connection.Close();
            Console.WriteLine("Database connection closed.");
        }
    }
}

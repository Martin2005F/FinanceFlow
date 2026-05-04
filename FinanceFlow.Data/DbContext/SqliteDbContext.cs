using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceFlow.Data.DbContext
{
    public class SqliteDbContext
    {
        private readonly string dbPath = @"D:\FinanceFlow\finance.db";
        private readonly string connectionString;

        public SqliteDbContext()
        {
            connectionString = $"Data source={dbPath}";
            InitializeDatabase();
        }

        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
        }

        public void InitializeDatabase()
        {
            string? folder = Path.GetDirectoryName(dbPath);
            if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            using (var connection = GetConnection())
            {
                string createCategories = @"
            CREATE TABLE IF NOT EXISTS Categories(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Icon TEXT,
                Type INTEGER NOT NULL);";

                string createTransaction = @"
            CREATE TABLE IF NOT EXISTS Transactions(
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Amount REAL NOT NULL,
                Description TEXT,
                Date TEXT NOT NULL,
                CategoryId INTEGER NOT NULL,
                FOREIGN KEY (CategoryId) REFERENCES Categories (Id));";

                connection.Execute(createCategories);
                connection.Execute(createTransaction);

                string seedCategory = "INSERT OR IGNORE INTO Categories (Id, Name, Type) VALUES (1, 'General', 0);";
                connection.Execute(seedCategory);
            }
        }
    }
}

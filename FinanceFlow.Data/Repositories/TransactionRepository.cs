using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper; 
using FinanceFlow.Data.DbContext;
using FinanceFlow.Domain.Interfaces;
using FinanceFlow.Domain.Models.Transactions; 
using FinanceFlow.Domain.Models.Categories;

namespace FinanceFlow.Data.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly SqliteDbContext context;

        public TransactionRepository(SqliteDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            using (var connection = context.GetConnection())
            {
                string sql = @"
                    SELECT t.*, c.* 
                    FROM Transactions t
                    INNER JOIN Categories c ON t.CategoryId = c.Id";

               

                var transactions = await connection.QueryAsync<Transaction, Category, Transaction>(
                    sql,
                    (transaction, category) =>
                    {
                        return new Transaction(
                            transaction.Id,
                            transaction.Amount,
                            transaction.Description,
                            transaction.CategoryId,
                            transaction.Date,
                            category);
                    },
                    splitOn: "Id" 
                );

                return transactions;
            }
        }

        public async Task AddAsync(Transaction transaction)
        {
            using (var connection = context.GetConnection())
            {
                string sql = @"INSERT INTO Transactions (Amount, Description, Date, CategoryId) 
                               VALUES (@Amount, @Description, @Date, @CategoryId)";

                await connection.ExecuteAsync(sql, new
                {
                    transaction.Amount,
                    transaction.Description,
                    Date = transaction.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    transaction.CategoryId
                });
            }
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            using (var connection = context.GetConnection())
            {
                string sql = @"UPDATE Transactions 
                               SET Amount = @Amount, Description = @Description, 
                                   Date = @Date, CategoryId = @CategoryId 
                               WHERE Id = @Id";

                await connection.ExecuteAsync(sql, new
                {
                    transaction.Id,
                    transaction.Amount,
                    transaction.Description,
                    Date = transaction.Date.ToString("yyyy-MM-dd HH:mm:ss"),
                    transaction.CategoryId
                });
            }
        }

        public async Task<decimal> GetTotalBalanceAsync()
        {
            using (var connection = context.GetConnection())
            {
                string sql = "SELECT COALESCE(SUM(Amount), 0) FROM Transactions";
                return await connection.ExecuteScalarAsync<decimal>(sql);
            }
        }

        public async Task<IEnumerable<Transaction>> GetByMonthAsync(int month, int year)
        {
            using (var connection = context.GetConnection())
            {
                string sql = @"
            SELECT t.*, c.* 
            FROM Transactions t
            INNER JOIN Categories c ON t.CategoryId = c.Id
            WHERE strftime('%m', t.Date) = @Month AND strftime('%Y', t.Date) = @Year";

                return await connection.QueryAsync<Transaction, Category, Transaction>(
                    sql,
                    (t, c) => new Transaction(t.Id, t.Amount, t.Description, t.CategoryId, t.Date, c),
                    new { Month = month.ToString("D2"), Year = year.ToString() },
                    splitOn: "Id"
                );
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = context.GetConnection())
            {
                await connection.ExecuteAsync("DELETE FROM Transactions WHERE Id = @Id", new { Id = id });
            }
        }
    }
}

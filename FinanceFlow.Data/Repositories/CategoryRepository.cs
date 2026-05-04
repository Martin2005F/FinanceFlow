using Dapper;
using FinanceFlow.Data.DbContext;
using FinanceFlow.Domain.Interfaces;
using FinanceFlow.Domain.Models.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceFlow.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqliteDbContext context;

        public CategoryRepository(SqliteDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync() 
        {
            using(var connection = context.GetConnection())
            {
                return await connection.QueryAsync<Category>("SELECT * FROM Categories");
            }
        }

        public async Task AddAsync(Category category)
        {
            using(var connection = context.GetConnection())
            {
                string sql = "INSTERT INTO Categories (Name, Icon, Type) VALUES (@Name, @Icon, @Type)";
                await connection.ExecuteAsync(sql, category);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var connection = context.GetConnection())
            {
                await connection.ExecuteAsync("DELETE FROM Categories WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            using (var connection = context.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Category>(
                    "SELECT * FROM Categories WHERE Id = @Id", new { Id = id });
            }
        }
    }
}

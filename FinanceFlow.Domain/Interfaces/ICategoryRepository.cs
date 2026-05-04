using FinanceFlow.Domain.Models.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceFlow.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task DeleteAsync(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using FinanceFlow.Domain.Models.Transactions; 

namespace FinanceFlow.Domain.Models.Transactions
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<IEnumerable<Transaction>> GetByMonthAsync(int month, int year);
        Task AddAsync(Transaction transaction);
        Task UpdateAsync(Transaction transaction);
        Task DeleteAsync(int id);
        Task<decimal> GetTotalBalanceAsync();
    }
}
using FinanceFlow.Domain.Models.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceFlow.Domain.Models.Transactions
{
    public interface ITransactionBuilder
    {
        Transaction Build();
        ITransactionBuilder SetId(int id);
        ITransactionBuilder SetAmount(decimal amount);
        ITransactionBuilder SetDescription(string description);
        ITransactionBuilder SetCategoryId(int categoryId);
        ITransactionBuilder SetDate(DateTime date);
        ITransactionBuilder SetCategory(Category category);
    }
}
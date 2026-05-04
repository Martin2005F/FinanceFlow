using FinanceFlow.Domain.Models.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceFlow.Domain.Models.Transactions
{
    public class TransactionBuilder : ITransactionBuilder
    {
        private int Id = 0;
        private decimal Amount = 200;
        private string Description = "No transaction";
        private int CategoryId = 0;
        private DateTime Date = DateTime.Now;
        private Category Category = null;


        public ITransactionBuilder SetId(int id)
        {
            this.Id = id;
            return this;
        }
        public ITransactionBuilder SetAmount(decimal amount)
        {
            this.Amount = amount;
            return this;
        }
        public ITransactionBuilder SetDescription(string description)
        {
            this.Description = description;
            return this;
        }
        public ITransactionBuilder SetCategoryId(int categoryId)
        {
            this.CategoryId = categoryId;
            return this;
        }
        public ITransactionBuilder SetDate(DateTime date)
        {
            this.Date = date;
            return this;
        }

        public ITransactionBuilder SetCategory(Category category)
        {
            this.Category = category;
            if (category != null) CategoryId = category.Id;
            return this;
        }

        public Transaction Build()
        {
            return new Transaction(Id, Amount, Description, CategoryId, Date, Category);
        }
    }
}

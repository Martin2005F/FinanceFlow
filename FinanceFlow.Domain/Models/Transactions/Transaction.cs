using System;
using System.Collections.Generic;
using System.Text;
using FinanceFlow.Domain.Models.Categories;
namespace FinanceFlow.Domain.Models.Transactions
{
    public class Transaction
    {
        public int Id { get; private set; }
        public decimal Amount { get; private set; }
        public string Description { get; private set; }
        public int CategoryId { get; private set; }
        public DateTime Date { get; private set; }
        public Category Category { get; private set; }

        public Transaction() { }
        public Transaction(int id, decimal amount, string description,
            int categoryId, DateTime date, Category category = null)
        {
            Id = id;
            Amount = amount;
            Description = description;
            CategoryId = categoryId;
            Date = date;
            Category = category;
        }

        public override string ToString()
        {
            return $"[{Date.ToShortDateString()}] {Amount}€ - {Description}";
        }

        public bool isValid()
        {
            return Amount > 0 && CategoryId > 0;
        }
    }
}

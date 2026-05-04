using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceFlow.Domain.Models.Categories  
{
    public enum TransactionType { Expense, Income }
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Icon { get; private set; }
        public TransactionType Type { get; private set; }

        public Category() { }
        public Category(int id, string name, string icon, TransactionType type)
        {
            Id = id;
            Name = name;
            Icon = icon;
            Type = type;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Icon: {Icon}";
        }
    }
}

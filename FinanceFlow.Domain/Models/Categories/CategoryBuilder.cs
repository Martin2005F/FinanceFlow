using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceFlow.Domain.Models.Categories  
{
   public class CategoryBuilder : ICategoryBuilder
    {
        private int Id = 0;
        private string Name = "No name";
        private string Icon = "👌";
        private TransactionType Type = TransactionType.Expense;
        public ICategoryBuilder SetId(int id)
        {
            this.Id = id;
            return this;
        }

        public ICategoryBuilder SetName(string name)
        {
            this.Name = name;
            return this;
        }

        public ICategoryBuilder SetIcon(string icon)
        {
            this.Icon = icon;
            return this;
        }

        public ICategoryBuilder SetType(TransactionType type)
        {
            this.Type = type;
            return this;
        }
        public Category Build()
        {
            return new Category(Id, Name, Icon, Type);
        }

    }
}

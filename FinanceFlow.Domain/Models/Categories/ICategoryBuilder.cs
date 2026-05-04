using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceFlow.Domain.Models.Categories
{
    public interface ICategoryBuilder
    {
        Category Build();
        ICategoryBuilder SetId(int id);
        ICategoryBuilder SetName(string name);
        ICategoryBuilder SetIcon(string icon);
        ICategoryBuilder SetType(TransactionType type);
    }
}

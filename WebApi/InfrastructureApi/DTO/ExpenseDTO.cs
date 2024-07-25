using ModelApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureApi.DTO
{
    public class ExpenseDTO : BallanseDTO
    {
        public TypeExpenseDTO? TypeExpense { get; set; }

        public static Expression<Func<Expense, ExpenseDTO>> ExpenseSelector
        {
            get
            {
                return expense => new ExpenseDTO()
                {
                    Id = expense.Id,
                    Amount = expense.Amount.Value,
                    CreateDate = expense.CreateDate.Value,
                    UpdateDate = expense.UpdateDate!.Value,
                    TypeId = expense.TypeId,
                    Comments = expense.Comments!.Value
                };
            }
        }
    }
}

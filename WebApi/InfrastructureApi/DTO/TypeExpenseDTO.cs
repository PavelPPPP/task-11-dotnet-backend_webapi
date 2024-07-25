using ModelApi.Entities;
using System.Linq.Expressions;

namespace InfrastructureApi.DTO
{
    public class TypeExpenseDTO : TypeBaseDTO
    {
        public List<ExpenseDTO>? Expenses { get; set; }

        public static Expression<Func<TypeExpense, TypeExpenseDTO>> TypeExpenseSelector
        {
            get
            {
                return typeExpense => new TypeExpenseDTO()
                {
                    Id = typeExpense.Id,
                    Name = typeExpense.Name.Value,
                    Description = typeExpense.Description!.Value,
                    CreateDate = typeExpense.CreateDate.Value,
                    UpdateDate = typeExpense.UpdateDate!.Value
                };
            }
        }
    }
}

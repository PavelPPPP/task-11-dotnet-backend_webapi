using ModelApi.Entities;
using System.Linq.Expressions;

namespace InfrastructureApi.DTO
{
    public class IncomeDTO : BallanseDTO
    {
        public TypeIncomeDTO? TypeIncome { get; set; }

        public static Expression<Func<Income, IncomeDTO>> IncomeSelector
        {
            get
            {
                return income => new IncomeDTO()
                {
                    Id = income.Id,
                    Amount = income.Amount.Value,
                    CreateDate = income.CreateDate.Value,
                    UpdateDate = income.UpdateDate!.Value,
                    TypeId = income.TypeId,
                    Comments = income.Comments!.Value
                };
            }
        }
    }
}

using ModelApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureApi.DTO
{
    public class TypeIncomeDTO : TypeBaseDTO
    {
        public List<IncomeDTO>? Incomes { get; set; }

        public static Expression<Func<TypeIncome, TypeIncomeDTO>> TypeIncomeSelector
        {
            get
            {
                return typeIncome => new TypeIncomeDTO()
                {
                    Id = typeIncome.Id,
                    Name = typeIncome.Name.Value,
                    Description = typeIncome.Description!.Value,
                    CreateDate = typeIncome.CreateDate.Value,
                    UpdateDate = typeIncome.UpdateDate!.Value
                };
            }
        }
    }
}

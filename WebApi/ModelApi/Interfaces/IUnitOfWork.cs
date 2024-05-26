using ModelApi.Entities;

namespace ModelApi.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ITypesBaseRepository<TypeIncome> TypesIncomes { get; }
        ITypesBaseRepository<TypeExpense> TypesExpenses { get; }
        //ITypesIncomesRepository<TypeIncome> TypesIncomes { get; }
        //ITypeExpenseRepository<TypeExpense> TypesExpenses { get; }
        //IIncomeRepository<Income> Incomes { get; }
        IBallanseRepository<Income> Incomes { get; }
        IBallanseRepository<Expense> Expenses { get; }
        //IExpenseRepository<Expense> Expenses { get; }

        Task Save();
    }
}

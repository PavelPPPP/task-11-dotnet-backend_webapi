using InfrastructureApi.Common;
using InfrastructureApi.DTO;
using InfrastructureApi.Interfaces;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.Services.Repositories;
using ModelApi.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureApi.Services
{
    public class ExpenseService : ModelService, IEntityService<ExpenseDTO>, IBallanseService<ExpenseDTO>
    {
        private readonly IBallanseRepository<Expense>? _expenseRepository;
        
        public ExpenseService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _expenseRepository = unitOfWork.Expenses;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetAllAsync()
        {
            var expenses = await _expenseRepository!.GetAllWithProjectionAsync(ExpenseDTO.ExpenseSelector);

            return expenses;
        }

        public async Task<ExpenseDTO> GetByIdAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var expense = await _expenseRepository!.GetByIdWithProjectionAsync(id, ExpenseDTO.ExpenseSelector);

            return expense!;
        }

        public async Task<double?> GetSumYesterdayAsync()
        {
            var result = await _expenseRepository!.GetSumYesterdayAsync();

            return result;
        }

        public async Task<double?> GetSumByPeriodAsync(DateTime fromDate, DateTime toDate)
        {
            var result = await _expenseRepository!.GetSumByPeriodAsync(fromDate, toDate);

            return result;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetByYesterdayAsync()
        {
            var result = await _expenseRepository!.GetByYesterdayWithDetailAndProjectionAsync(ExpenseDTO.ExpenseSelector);

            return result;
        }

        public async Task<IEnumerable<ExpenseDTO>> GetByPeriodAsync(DateTime fromDate, DateTime toDate)
        {
            var result = await _expenseRepository!.GetByPeriodWithDetailAndProjectionAsync(fromDate, toDate, ExpenseDTO.ExpenseSelector);

            return result;
        }

        public async Task CreateAsync(ExpenseDTO expenseDTO)
        {
            if (expenseDTO is null) throw new ArgumentNullException(nameof(expenseDTO));

            var expense = new Expense(new Amount(expenseDTO.Amount), expenseDTO.TypeId, new FreeText(expenseDTO.Comments));

            await _expenseRepository!.CreateAsync(expense);
            await _unitOfWork!.SaveAsync();
        }

        public async Task UpdateAsync(ExpenseDTO expenseDTO)
        {
            if (expenseDTO is null) throw new ArgumentNullException(nameof(expenseDTO));

            var expense = await _expenseRepository!.GetByIdAsync(expenseDTO.Id);
            expense.Change(new Amount(expenseDTO.Amount), expenseDTO.TypeId, new FreeText(expenseDTO.Comments));

            _expenseRepository.Update(expense);
            await _unitOfWork!.SaveAsync();
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var expense = await _expenseRepository!.GetByIdAsync(id);

            _expenseRepository.Delete(expense);
            await _unitOfWork!.SaveAsync();
        }
    }
}

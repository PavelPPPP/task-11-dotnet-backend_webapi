using Microsoft.EntityFrameworkCore;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.Services.DataSource;
using System.Linq.Expressions;

namespace ModelApi.Services.Repositories
{
    public class ExpenseRepository : BaseRepository, IBallanseRepository<Expense>
    {
        public ExpenseRepository(SelfFinanceDbContext dbContext)
            : base(dbContext) { }

        public async Task<IEnumerable<Expense>> GetAllAsync()
        {
            var result = await _dbContext.Expenses.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TResult>> GetAllWithProjectionAsync<TResult>(Expression<Func<Expense, TResult>> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }


            var result = await _dbContext.Expenses
                .Select(selector)
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Expense>> GetWithFilterByIdAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var result = await _dbContext.Expenses
                .Where(ti => ti.Id >= id)
                .ToListAsync();

            return result;
        }

        public async Task<Expense> GetByIdAsync(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var result = await _dbContext.Expenses
                .Where(ti => ti.Id == id)
                .SingleOrDefaultAsync();

            return result!;
        }

        public async Task<TResult?> GetByIdWithProjectionAsync<TResult>(int? id, Expression<Func<Expense, TResult>> selector)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            var result = await _dbContext.Expenses
                .Where(ti => ti.Id == id)
                .Select(selector)
                .SingleOrDefaultAsync();

            return result;
        }

        public async Task<double?> GetSumYesterdayAsync()
        {
            DateTime dateNow = DateTime.Now;

            var result = await _dbContext.Expenses
                .Where(IsYesterdayDay())
                .SumAsync(i => i.Amount.Value);

            return result;
        }

        public async Task<IEnumerable<TResult>> GetByYesterdayWithDetailAndProjectionAsync<TResult>(Expression<Func<Expense, TResult>> selector)
        {
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            var result = await _dbContext.Expenses
                .Include(i => i.TypeExpense)
                .Where(IsYesterdayDay())
                .Select(selector)
                .ToListAsync();

            return result;
        }

        public async Task<double?> GetSumByPeriodAsync(DateTime fromDate, DateTime toDate)
        {
            var result = await _dbContext.Expenses
                .Where(IsByPeriod(fromDate, toDate))
                .SumAsync(i => i.Amount.Value);

            return result;
        }

        public async Task<IEnumerable<TResult>> GetByPeriodWithDetailAndProjectionAsync<TResult>(DateTime fromDate, DateTime toDate, Expression<Func<Expense, TResult>> selector)
        {
            if (selector is null) throw new ArgumentNullException(nameof(selector));

            var result = await _dbContext.Expenses
                .Include(ti => ti.TypeExpense)
                .Where(IsByPeriod(fromDate, toDate))
                .Select(selector)
                .ToListAsync();

            return result;
        }

        public async Task CreateAsync(Expense expense)
        {
            if (expense is null)
            {
                throw new ArgumentNullException(nameof(expense));
            }

            await _dbContext.Expenses.AddAsync(expense);
        }

        public void Update(Expense expense)
        {
            if (expense is null)
            {
                throw new ArgumentNullException(nameof(expense));
            }

            _dbContext.Expenses.Update(expense);
        }

        public void Delete(Expense expense)
        {
            if (expense is null)
            {
                throw new ArgumentNullException(nameof(expense));
            }

            _dbContext.Expenses.Remove(expense);
        }

        private Expression<Func<Expense, bool>> IsYesterdayDay()
        {
            return (i) => i.CreateDate.Value.Year == DateTime.Now.Year
                && i.CreateDate.Value.Month == DateTime.Now.Month
                && i.CreateDate.Value.Day == (DateTime.Now.Day - 1);
        }

        private Expression<Func<Expense, bool>> IsByPeriod(DateTime fromDate, DateTime toDate)
        {
            return i => i.CreateDate.Value.Year >= fromDate.Year
                && i.CreateDate.Value.Month >= fromDate.Month
                && i.CreateDate.Value.Day >= fromDate.Day
                && i.CreateDate.Value.Year <= toDate.Year
                && i.CreateDate.Value.Month <= toDate.Month
                && i.CreateDate.Value.Day <= toDate.Day;
        }
    }
}

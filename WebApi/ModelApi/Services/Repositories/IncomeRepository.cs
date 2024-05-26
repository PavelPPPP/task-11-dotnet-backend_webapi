using Microsoft.EntityFrameworkCore;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.Services.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ModelApi.Services.Repositories
{
    public class IncomeRepository : BaseRepository, IBallanseRepository<Income>
    {
        public IncomeRepository(SelfFinanceDbContext dbContext)
            : base(dbContext) { }

        public async Task<IEnumerable<Income>> GetAll()
        {
            var result = await _dbContext.Incomes.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Income>> GetWithFilterById(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var result = await _dbContext.Incomes
                .Where(ti => ti.Id >= id)
                .ToListAsync();

            return result;
        }

        public async Task<Income> GetById(int? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var result = await _dbContext.Incomes
                .Where(ti => ti.Id == id)
                .SingleOrDefaultAsync();

            return result!;
        }

        public async Task<double?> GetSumYesterday()
        {
            DateTime dateNow = DateTime.Now;

            var result = await _dbContext.Incomes
                .Where(IsYesterdayDay())
                .SumAsync(i => i.Amount.Value);

            return result;
        }

        public async Task<IEnumerable<Income>> GetByYesterdayWithDetail()
        {
            var result = await _dbContext.Incomes
                .Include(i => i.TypeIncome)
                .Where(IsYesterdayDay())
                .ToListAsync();

            return result;
        }

        public async Task<double?> GetSumByPeriod(DateTime fromDate, DateTime toDate)
        {
            var result = await _dbContext.Incomes
                .Where(IsByPeriod(fromDate, toDate))
                .SumAsync (i => i.Amount.Value);

            return result;
        }

        public async Task<IEnumerable<Income>> GetByPeriodWithDetail(DateTime fromDate, DateTime toDate)
        {
            var result = await _dbContext.Incomes.Include(ti => ti.TypeIncome)
                .Where(IsByPeriod(fromDate, toDate))
                .ToListAsync();

            return result;
        }

        public async Task CreateAsync(Income income)
        {
            if (income is null)
            {
                throw new ArgumentNullException(nameof(income));
            }

            await _dbContext.Incomes.AddAsync(income);
        }

        public void Update(Income income)
        {
            if (income is null)
            {
                throw new ArgumentNullException(nameof(income));
            }

            _dbContext.Incomes.Update(income);
        }

        public void Delete(Income income)
        {
            if (income is null)
            {
                throw new ArgumentNullException(nameof(income));
            }

            _dbContext.Incomes.Remove(income);
        }

        private Expression<Func<Income, bool>> IsYesterdayDay()
        {
            return (i) => i.CreateDate.Value.Year == DateTime.Now.Year
                && i.CreateDate.Value.Month == DateTime.Now.Month
                && i.CreateDate.Value.Day == (DateTime.Now.Day - 1);
        }

        private Expression<Func<Income, bool>> IsByPeriod(DateTime fromDate, DateTime toDate)
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

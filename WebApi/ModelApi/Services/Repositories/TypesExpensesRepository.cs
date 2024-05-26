using Microsoft.EntityFrameworkCore;
using ModelApi.Entities;
using ModelApi.Interfaces;
using ModelApi.Services.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelApi.Services.Repositories
{
    public class TypesExpensesRepository : BaseRepository, ITypesBaseRepository<TypeExpense>
    {
        public TypesExpensesRepository(SelfFinanceDbContext dbContext) 
            : base(dbContext) { }

        public async Task<IEnumerable<TypeExpense>> GetAll()
        {
            var result = await _dbContext.TypesExpenses.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TypeExpense>> GetWithFilterById(int? typeId)
        {
            if (typeId == null) throw new ArgumentNullException(nameof(typeId));

            var result = await _dbContext.TypesExpenses
                .Where(ti => ti.Id >= typeId)
                .ToListAsync();

            return result;
        }

        public async Task<TypeExpense> GetById(int? typeId)
        {
            if (typeId == null) throw new ArgumentNullException(nameof(typeId));

            var result = await _dbContext.TypesExpenses
                .Where(ti => ti.Id == typeId)
                .SingleOrDefaultAsync();

            return result!;
        }

        public async Task CreateAsync(TypeExpense typesExpenses)
        {
            if (typesExpenses is null)
            {
                throw new ArgumentNullException(nameof(typesExpenses));
            }

            await _dbContext.TypesExpenses.AddAsync(typesExpenses);
        }

        public void Update(TypeExpense typesExpenses)
        {
            if (typesExpenses is null)
            {
                throw new ArgumentNullException(nameof(typesExpenses));
            }

            _dbContext.TypesExpenses.Update(typesExpenses);
        }

        public void Delete(TypeExpense typesExpenses)
        {
            if (typesExpenses is null)
            {
                throw new ArgumentNullException(nameof(typesExpenses));
            }

            _dbContext.TypesExpenses.Remove(typesExpenses);
        }
    }
}

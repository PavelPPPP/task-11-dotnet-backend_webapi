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
    public class TypesIncomesRepository : BaseRepository, ITypesBaseRepository<TypeIncome>
    {
        public TypesIncomesRepository(SelfFinanceDbContext dbContext)
            : base(dbContext) { }

        public async Task<IEnumerable<TypeIncome>> GetAll()
        {
            var result = await _dbContext.TypesIncomes.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TypeIncome>> GetWithFilterById(int? typeId)
        {
            if (typeId == null) throw new ArgumentNullException(nameof(typeId));

            var result = await _dbContext.TypesIncomes
                .Where(ti => ti.Id >= typeId)
                .ToListAsync();

            return result;
        }

        public async Task<TypeIncome> GetById(int? typeId)
        {
            if (typeId == null) throw new ArgumentNullException(nameof(typeId));

            var result = await _dbContext.TypesIncomes
                .Where(ti => ti.Id == typeId)
                .SingleOrDefaultAsync();

            return result!;
        }

        public async Task CreateAsync(TypeIncome typesIncomes)
        {
            if (typesIncomes is null)
            {
                throw new ArgumentNullException(nameof(typesIncomes));
            }

            await _dbContext.TypesIncomes.AddAsync(typesIncomes);
        }

        public void Update(TypeIncome typesIncomes)
        {
            if (typesIncomes is null)
            {
                throw new ArgumentNullException(nameof(typesIncomes));
            }

            _dbContext.TypesIncomes.Update(typesIncomes);
        }

        public void Delete(TypeIncome typesIncomes)
        {
            if (typesIncomes is null)
            {
                throw new ArgumentNullException(nameof(typesIncomes));
            }

            _dbContext.TypesIncomes.Remove(typesIncomes);
        }
    }
}

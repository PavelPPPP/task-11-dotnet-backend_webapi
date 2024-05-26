using ModelApi.Interfaces;
using ModelApi.Services.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelApi.Services.Repositories
{
    public abstract class BaseRepository 
    {
        protected readonly SelfFinanceDbContext _dbContext;

        public BaseRepository(SelfFinanceDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}

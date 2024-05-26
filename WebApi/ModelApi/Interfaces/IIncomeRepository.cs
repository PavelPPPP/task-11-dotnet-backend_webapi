using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelApi.Interfaces
{
    public interface IIncomeRepository<T> : IRepository<T> where T : class
    {
    }
}

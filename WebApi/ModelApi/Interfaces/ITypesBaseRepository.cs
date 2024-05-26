using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelApi.Interfaces
{
    public interface ITypesBaseRepository<T> : IRepository<T> where T : class
    {
    }
}

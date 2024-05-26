using ModelApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelApi.Interfaces
{
    public interface ITypesIncomesRepository<T> : IRepository<T> where T : class
    {
    }
}

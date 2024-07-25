using InfrastructureApi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureApi.Interfaces
{
    public interface ITypeBaseService<T> : IEntityService<T> where T : class
    {
    }
}

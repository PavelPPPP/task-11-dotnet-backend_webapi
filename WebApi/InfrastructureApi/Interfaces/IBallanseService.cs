using InfrastructureApi.Common;
using InfrastructureApi.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureApi.Interfaces
{
    public interface IBallanseService<T>
    {
        Task<double?> GetSumYesterdayAsync();
        Task<double?> GetSumByPeriodAsync(DateTime fromDate, DateTime toDate);
        Task<IEnumerable<T>> GetByYesterdayAsync();
        Task<IEnumerable<T>> GetByPeriodAsync(DateTime fromDate, DateTime toDate);
    }
}

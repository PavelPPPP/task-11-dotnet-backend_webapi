using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelApi.Interfaces
{
    public interface IBallanseRepository<T> : IRepository<T> where T : class
    {
        Task<double?> GetSumYesterday();
        Task<IEnumerable<T>> GetByYesterdayWithDetail();
        Task<double?> GetSumByPeriod(DateTime fromDate, DateTime toDate);
        Task<IEnumerable<T>> GetByPeriodWithDetail(DateTime fromDate, DateTime toDate);
    }
}

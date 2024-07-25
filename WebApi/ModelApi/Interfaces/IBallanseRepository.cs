using ModelApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ModelApi.Interfaces
{
    public interface IBallanseRepository<T> : IRepository<T> where T : class
    {
        Task<double?> GetSumYesterdayAsync();
        //Task<IEnumerable<T>> GetByYesterdayWithDetailAsync();
        Task<double?> GetSumByPeriodAsync(DateTime fromDate, DateTime toDate);
        //Task<IEnumerable<T>> GetByPeriodWithDetailAsync(DateTime fromDate, DateTime toDate);

        Task<IEnumerable<TResult>> GetByYesterdayWithDetailAndProjectionAsync<TResult>(Expression<Func<T, TResult>> selector);
        Task<IEnumerable<TResult>> GetByPeriodWithDetailAndProjectionAsync<TResult>(DateTime fromDate, DateTime toDate, Expression<Func<T, TResult>> selector);
    }
}

using System.Linq.Expressions;

namespace ModelApi.Interfaces
{
    public interface IBallanseRepository<T> : IRepository<T> where T : class
    {
        Task<double?> GetSumYesterdayAsync();
        Task<double?> GetSumByPeriodAsync(DateTime fromDate, DateTime toDate);
        Task<IEnumerable<TResult>> GetByYesterdayWithDetailAndProjectionAsync<TResult>(Expression<Func<T, TResult>> selector);
        Task<IEnumerable<TResult>> GetByPeriodWithDetailAndProjectionAsync<TResult>(DateTime fromDate, DateTime toDate, Expression<Func<T, TResult>> selector);
    }
}

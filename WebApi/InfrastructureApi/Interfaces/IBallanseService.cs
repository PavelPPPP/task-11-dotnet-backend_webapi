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

namespace ModelApi.Interfaces
{
    public interface ITypesBaseRepository<T> : IRepository<T> where T : class
    {
        Task<T> GetByIdWithDetailAsync(int? id);
    }
}

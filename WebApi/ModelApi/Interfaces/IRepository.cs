namespace ModelApi.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetWithFilterById(int? id);
        Task<T> GetById(int? id);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

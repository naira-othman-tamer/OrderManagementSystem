

namespace OrderManagementSystem.Core.Abstractions.Repositories
{
    public interface IGenericRepository<T, TKey> where T : BaseModel<TKey>
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<T> GetByIdAsync(TKey id);
        Task<T?> GetByIdWithTrackingAsync(TKey id);

        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        void Update(T entity);

        Task UpdateIncludeAsync(T entity, params string[] modifiedParams);
        Task DeleteAsync(TKey id);
        Task DeleteRangeAsync(IEnumerable<TKey> ids);

    }
}


namespace OrderManagementSystem.Infrastructure.Repositories
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey>
    where T : BaseModel<TKey>
    {
        private readonly OrderManagementDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(OrderManagementDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        #region Read

        public async Task<IQueryable<T>> GetAllAsync()
        {
            IQueryable<T>? entities = _dbSet.Where(e => !e.IsDeleted).AsQueryable();
            return await Task.FromResult(entities);

        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id!.Equals(id) && !e.IsDeleted);
            return entity!;
        }

        public async Task<T?> GetByIdWithTrackingAsync(TKey id)
        {
            return await _dbSet.AsTracking()
                .FirstOrDefaultAsync(x => x.Id!.Equals(id) && !x.IsDeleted);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(x => !x.IsDeleted).Where(expression);
        }

        #endregion
        public async Task AddAsync(T entity)
        {
            entity.CreatedAt = DateTime.Now;
            await _dbSet.AddAsync(entity);
        }

        #region Update

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task UpdateIncludeAsync(T entity, params string[] modifiedParams)
        {
            if (!await _dbSet.AnyAsync(x => x.Id!.Equals(entity.Id) && !x.IsDeleted))
                return;

            var local = _dbSet.Local.FirstOrDefault(x => x.Id!.Equals(entity.Id));
            EntityEntry<T> entityEntry;

            if (local == null)
            {
                entityEntry = _context.Entry(entity);
            }
            else
            {
                entityEntry = _context.ChangeTracker.Entries<T>()
                    .FirstOrDefault(x => x.Entity.Id!.Equals(entity.Id))!;
            }

            foreach (var prop in entityEntry.Properties)
            {
                if (modifiedParams.Contains(prop.Metadata.Name))
                {
                    var value = entity.GetType().GetProperty(prop.Metadata.Name)?.GetValue(entity);
                    prop.CurrentValue = value;
                    prop.IsModified = true;
                }
            }
        }

        #endregion

        #region SoftDelete

        public async Task DeleteAsync(TKey id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null && !entity.IsDeleted)
            {
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
                _dbSet.Update(entity);
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<TKey> ids)
        {
            var query = await GetAllAsync();
            var entities = await query.Where(x => ids.Contains(x.Id)).ToListAsync();

            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.Now;
                entity.UpdatedAt = DateTime.Now;
            }

            _dbSet.UpdateRange(entities);
        }

        #endregion


    }
}

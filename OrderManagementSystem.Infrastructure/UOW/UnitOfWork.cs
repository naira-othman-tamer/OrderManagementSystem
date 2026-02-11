
namespace OrderManagementSystem.Infrastructure.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrderManagementDbContext _context;
        private IDbContextTransaction? _transaction;

        #region Private fields for repositories

        private IGenericRepository<Customer, int>? _customerRepository;
        private IGenericRepository<Order, int>? _orderRepository;
        private IGenericRepository<OrderItem, int>? _orderItemRepository;
        private IGenericRepository<Product, int>? _productRepository;
        private IGenericRepository<Invoice, int>? _invoiceRepository;
        private IGenericRepository<User, Guid>? _userRepository;

        #endregion

        public UnitOfWork(OrderManagementDbContext context)
        {
            _context = context;
        }

        #region Repository Properties with Lazy Initialization

        public IGenericRepository<Customer, int> CustomerRepository
            => _customerRepository ??= new GenericRepository<Customer, int>(_context);

        public IGenericRepository<Order, int> OrderRepository
            => _orderRepository ??= new GenericRepository<Order, int>(_context);

        public IGenericRepository<OrderItem, int> OrderItemRepository
            => _orderItemRepository ??= new GenericRepository<OrderItem, int>(_context);

        public IGenericRepository<Product, int> ProductRepository
            => _productRepository ??= new GenericRepository<Product, int>(_context);

        public IGenericRepository<Invoice, int> InvoiceRepository
            => _invoiceRepository ??= new GenericRepository<Invoice, int>(_context);

        public IGenericRepository<User, Guid> UserRepository
            => _userRepository ??= new GenericRepository<User, Guid>(_context);

        #endregion

        #region SaveChanges with automatic timestamps

        public async Task<int> SaveChangesAsync()
        {
            var entries = _context.ChangeTracker.Entries()
                .Where(e => e.Entity is BaseModel<int> || e.Entity is BaseModel<Guid>);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is BaseModel<int> entityInt)
                        entityInt.CreatedAt = DateTime.Now;
                    else if (entry.Entity is BaseModel<Guid> entityGuid)
                        entityGuid.CreatedAt = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is BaseModel<int> entityInt)
                        entityInt.UpdatedAt = DateTime.Now;
                    else if (entry.Entity is BaseModel<Guid> entityGuid)
                        entityGuid.UpdatedAt = DateTime.Now;
                }
            }

            return await _context.SaveChangesAsync();
        }

        #endregion

        #region Transaction Management

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        #endregion

        #region IAsyncDisposable Implementation
        public async ValueTask DisposeAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
            }
            await _context.DisposeAsync();
        }
        #endregion


    }
}

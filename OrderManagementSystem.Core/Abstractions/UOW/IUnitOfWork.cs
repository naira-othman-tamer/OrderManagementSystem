
namespace OrderManagementSystem.Core.Abstractions.UOW
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<Customer, int> CustomerRepository { get; }
        IGenericRepository<Order, int> OrderRepository { get; }
        IGenericRepository<OrderItem, int> OrderItemRepository { get; }
        IGenericRepository<Product, int> ProductRepository { get; }
        IGenericRepository<Invoice, int> InvoiceRepository { get; }
        IGenericRepository<User, Guid> UserRepository { get; }

        #region Transaction Management

        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        #endregion
    }
}

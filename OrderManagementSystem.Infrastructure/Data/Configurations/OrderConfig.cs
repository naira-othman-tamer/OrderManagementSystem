
namespace OrderManagementSystem.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasQueryFilter(o => !o.IsDeleted);

            // Decimal
            builder.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            // Enum as String
            builder.Property(o => o.Status)
                .HasConversion(
                  o => o.ToString(),
                    s => (Status)Enum.Parse(typeof(Status), s)
                );


            builder.Property(o => o.PaymentMethod)
                .HasConversion(
                    pm => pm.ToString(),
                    s => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), s)
                );

            // Relationships
            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

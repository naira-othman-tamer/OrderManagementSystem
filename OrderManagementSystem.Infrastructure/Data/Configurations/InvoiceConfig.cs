
namespace OrderManagementSystem.Infrastructure.Data.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasQueryFilter(i => !i.IsDeleted);


            builder.Property(i => i.InvoiceDate)
                .IsRequired();

            builder.Property(i => i.TotalAmount)
                .HasColumnType("decimal(18,2)");

            // Relationship with Order
            builder.HasOne(i => i.Order)
                .WithOne()
                .HasForeignKey<Invoice>(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

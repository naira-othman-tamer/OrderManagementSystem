
namespace OrderManagementSystem.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            // builder.Ignore(c=>c.CustomerId);

            builder.HasKey(c => c.Id);

            builder.HasQueryFilter(c => !c.IsDeleted);


            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(100);

            // Relationship with User
            builder.HasOne(c => c.User)
                .WithOne(u => u.Customer)
                .HasForeignKey<Customer>(c => c.UserId);
        }
    }
}

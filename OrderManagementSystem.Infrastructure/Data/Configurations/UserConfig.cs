
namespace OrderManagementSystem.Infrastructure.Data.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasQueryFilter(u => !u.IsDeleted);

            builder.Property(u => u.Username).IsRequired().HasMaxLength(50);
            builder.Property(u => u.PasswordHash).IsRequired();

            // Enum as String
            builder.Property(u => u.Role)
                .HasConversion(
                    role => role.ToString(),
                    roleString => (Role)Enum.Parse(typeof(Role), roleString)
                );
        }
    }
}

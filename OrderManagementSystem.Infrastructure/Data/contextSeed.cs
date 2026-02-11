using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrderManagementSystem.Infrastructure.Data
{

    public static class contextSeed
    {

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNameCaseInsensitive = true
        };

        public static async Task SeedAsync(OrderManagementDbContext context)
        {

            #region Seed products
            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../OrderManagementSystem.Infrastructure/Data/DataSeed/Products.json");
                List<Product>? products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await context.Products.AddAsync(product);
                    }
                    await context.SaveChangesAsync();
                }
            }
            #endregion

            #region Seed Users
            if (!await context.Users.AnyAsync())
            {
                var users = new List<User>
                {
                  new User
                  {
                   Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                 Username = "admin",
                 PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                 Role = Role.Admin
                 },
                  new User
                {
                     Id = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f12345678901"),
                     Username = "customer1",
                     PasswordHash = BCrypt.Net.BCrypt.HashPassword("Customer@123"),
                     Role = Role.Customer
                 }
                 };
                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
            }
            #endregion

            #region Seed Customers
            if (!await context.Customers.AnyAsync())
            {
                var customersData = File.ReadAllText("../OrderManagementSystem.Infrastructure/Data/DataSeed/Customers.json");
                List<Customer>? customers = JsonSerializer.Deserialize<List<Customer>>(customersData);
                if (customers?.Count > 0)
                {
                    foreach (var customer in customers)
                    {
                        await context.Customers.AddAsync(customer);
                    }
                    await context.SaveChangesAsync();
                }
            }
            #endregion

            #region Seed Orders
            if (!await context.Orders.AnyAsync())
            {
                var ordersData = File.ReadAllText("../OrderManagementSystem.Infrastructure/Data/DataSeed/Orders.json");
                // List<Order>? orders = JsonSerializer.Deserialize<List<Order>>(ordersData);
                List<Order>? orders = JsonSerializer.Deserialize<List<Order>>(ordersData, _jsonOptions);

                if (orders?.Count > 0)
                {
                    foreach (var order in orders)
                    {
                        await context.Orders.AddAsync(order);
                    }
                    await context.SaveChangesAsync();
                }
            }
            #endregion

            #region Seed OrderItems
            if (!await context.OrderItems.AnyAsync())
            {
                var orderItemsData = File.ReadAllText("../OrderManagementSystem.Infrastructure/Data/DataSeed/OrderItems.json");
                List<OrderItem>? orderItems = JsonSerializer.Deserialize<List<OrderItem>>(orderItemsData);
                if (orderItems?.Count > 0)
                {
                    foreach (var orderItem in orderItems)
                    {
                        await context.OrderItems.AddAsync(orderItem);
                    }
                    await context.SaveChangesAsync();
                }
            }
            #endregion

            #region Seed Invoices
            if (!await context.Invoices.AnyAsync())
            {
                var invoicesData = File.ReadAllText("../OrderManagementSystem.Infrastructure/Data/DataSeed/Invoices.json");
                List<Invoice>? invoices = JsonSerializer.Deserialize<List<Invoice>>(invoicesData);
                if (invoices?.Count > 0)
                {
                    foreach (var invoice in invoices)
                    {
                        await context.Invoices.AddAsync(invoice);
                    }
                    await context.SaveChangesAsync();
                }
            }
            #endregion


        }
    }
}

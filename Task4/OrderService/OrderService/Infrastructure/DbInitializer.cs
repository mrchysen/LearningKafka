using Microsoft.EntityFrameworkCore;
using OrderService.Domain;

namespace OrderService.Infrastructure;

public static class DbInitializer
{
    public static async Task SeedAsync(OrderServiceDbContext dbContext)
    {
        if (!await dbContext.Products.AnyAsync())
        {
            var products = new List<Product>
            {
                new() { Id = Guid.NewGuid(), Name = "Говядина", Quantity = 100, Price = 550.50m },
                new() { Id = Guid.NewGuid(), Name = "Макароны", Quantity = 100, Price = 89.90m },
                new() { Id = Guid.NewGuid(), Name = "Гречневая крупа", Quantity = 20, Price = 120.75m },
                new() { Id = Guid.NewGuid(), Name = "Рисовая крупа", Quantity = 1, Price = 95.30m },
                new() { Id = Guid.NewGuid(), Name = "Чипсы", Quantity = 999, Price = 180.25m },
                new() { Id = Guid.NewGuid(), Name = "Арбуз", Quantity = 20, Price = 75.00m }
            };

            await dbContext.Products.AddRangeAsync(products);
            await dbContext.SaveChangesAsync();
        }
    }
}

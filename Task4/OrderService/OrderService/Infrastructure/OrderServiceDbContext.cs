using Microsoft.EntityFrameworkCore;
using OrderService.Domain;

namespace OrderService.Infrastructure;

public class OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<OrderItem>().HasData(
            new () { Id = Guid.NewGuid(), Name = "Говядина", Quantity = 100 },
            new() { Id = Guid.NewGuid(), Name = "Макароны", Quantity = 100 },
            new() { Id = Guid.NewGuid(), Name = "Гречневая крупа", Quantity = 20 },
            new() { Id = Guid.NewGuid(), Name = "Рисовая крупа", Quantity = 1 },
            new() { Id = Guid.NewGuid(), Name = "Чипсы", Quantity = 999 },
            new() { Id = Guid.NewGuid(), Name = "Арбуз", Quantity = 20 }
        );
    }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }
}

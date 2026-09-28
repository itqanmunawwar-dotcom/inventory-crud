using Inventory.Web.Models;

namespace Inventory.Web.Data;

public static class SeedData
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (context.Products.Any()) return;

        context.Products.AddRange(
            new Product { Name = "Wireless Keyboard", Description = "Compact keyboard for shared workspaces.", Price = 49.99m, StockQuantity = 18 },
            new Product { Name = "USB-C Dock", Description = "Dual-display dock with power delivery.", Price = 129.00m, StockQuantity = 7 },
            new Product { Name = "Noise-cancelling Headset", Description = "Over-ear headset for calls and focused work.", Price = 89.50m, StockQuantity = 12 });

        context.SaveChanges();
    }
}

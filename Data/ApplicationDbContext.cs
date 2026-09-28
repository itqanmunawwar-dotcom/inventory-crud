using Inventory.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Web.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
}

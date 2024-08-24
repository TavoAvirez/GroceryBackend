using Microsoft.EntityFrameworkCore;

public class ProductsContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public ProductsContext(DbContextOptions<ProductsContext> options)
            : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=products.db");
    }
}
using CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Context;
public class AppDbContext : DbContext
{
    public DbSet<Category> categories {  get; set; }
    public DbSet<Product> products { get; set; }
    public DbSet<Customer> customers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

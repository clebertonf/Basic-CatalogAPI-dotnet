using CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Context;
public class AppDbContext : DbContext
{
    DbSet<Category> categories {  get; set; }
    DbSet<Product> products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}

using CatologService.Models;
using Microsoft.EntityFrameworkCore;
namespace CatologService.Data
{
    public class CatalogDbContext:DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
    }
}

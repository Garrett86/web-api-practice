using Microsoft.EntityFrameworkCore;

namespace Test_Web_API.Entities
{
    public class NorthwindDbContext : DbContext
    {
        public DbSet<Supplier> Suppliers { get; set; }

        public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options):base(options)
        {

        }
    }
}

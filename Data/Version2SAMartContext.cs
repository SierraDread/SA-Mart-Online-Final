using Microsoft.EntityFrameworkCore;
using Version2SAMart.Models;  // Ensure this matches your models' namespace

namespace Version2SAMart.Data
{
    public class Version2SAMartContext : DbContext
    {
        public Version2SAMartContext(DbContextOptions<Version2SAMartContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}



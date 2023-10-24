using CartMicroservice.Models;
using Microsoft.EntityFrameworkCore;

namespace CartMicroservice.DbContexts
{
    public class CartDbContext : DbContext
    {
        public CartDbContext(DbContextOptions<CartDbContext> options) : base(options)
        {

        }


        public DbSet<Cart> Cart { get; set; }
    }
}

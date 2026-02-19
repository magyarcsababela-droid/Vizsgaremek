using ComputerpartsLibrary.MODEL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputerpartsLibrary.DATA
{
    public class ComputerpatsDbContext : DbContext
    {
        public ComputerpatsDbContext() { }
        public ComputerpatsDbContext(DbContextOptions<ComputerpatsDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}

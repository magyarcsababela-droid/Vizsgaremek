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
        public DbSet<Users> Users { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
    }
}

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
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<Build_components> Build_components { get; set; }
        public DbSet<Custom_builds> Custom_builds { get; set; }
        public DbSet<Components> Components { get; set; }
        public DbSet<Component_type> Component_types { get; set; }
        public DbSet<Order_items_p> Order_items_p { get; set; }
        public DbSet<Order_items_b> Order_items_b { get; set; }
        public DbSet<Prebuilt_pcs> Prebuilt_pcs { get; set; }
        public DbSet<Prebuilt_pc_comp> Prebuilt_pc_comp { get; set; }
        public DbSet<Inventory_components> Inventory_components { get; set; }
        public DbSet<Inventory_products> Inventory_products { get; set; }
    }
}

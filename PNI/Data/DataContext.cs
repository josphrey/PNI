using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PNI.Entities;

namespace PNI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Amortization> Amortization { get; set; }
        public DbSet<Buyers> Buyers { get; set; }
        public DbSet<Unit> Unit { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Amortization>().ToTable("Amortization");
        //    modelBuilder.Entity<Buyers>().ToTable("Buyers");
        //    modelBuilder.Entity<Unit>().ToTable("Unit");
        //}
    }
}

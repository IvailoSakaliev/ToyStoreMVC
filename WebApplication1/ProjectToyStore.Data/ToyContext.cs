using Microsoft.EntityFrameworkCore;
using ProjectToyStore.Data.Config;
using ProjectToyStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectToyStore.Data
{
    public class ToyContext
        : DbContext, IToyContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Product> Subjects { get; set; }
        public DbSet<Hash> Hashs { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configurate.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

    }
}

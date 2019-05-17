using Microsoft.EntityFrameworkCore;
using ProjectToyStore.Data.Models;

namespace ProjectToyStore.Data
{
    public interface IToyContext
    {
        DbSet<Login> Logins { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Images> Images { get; set; }
        DbSet<Product> Subjects { get; set; }


        void SaveChanges();
    }
}
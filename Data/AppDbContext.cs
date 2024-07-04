
using Microsoft.EntityFrameworkCore;
using MVC_Project2024.Models;


namespace MVC_Project2024.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
  
               public DbSet<UserRegistration> userRegistrations { get; set; }

               public DbSet<UserLog> userLogs { get; set; }

    }
}

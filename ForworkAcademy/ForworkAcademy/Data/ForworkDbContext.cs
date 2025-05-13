using ForworkAcademy.Models;
using Microsoft.EntityFrameworkCore;

namespace ForworkAcademy.Data
{
    public class ForworkDbContext : DbContext
    {
        public DbSet<Course> Course{ get; set; }
        public DbSet<Category> Category{ get; set; }
        public DbSet<Lecturer> Lecturer { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<UserPopup> UserPopups { get; set; }
        public ForworkDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var admin = new Admin
            {
                Id = 1,
                UserName = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123")
            };

            modelBuilder.Entity<Admin>().HasData(admin);
        }
    }
}

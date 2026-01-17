using Microsoft.EntityFrameworkCore;
using Lab1_StudentAPI.Models;

namespace Lab1_StudentAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Seed data - Dữ liệu mẫu
            modelBuilder.Entity<Student>().HasData(
            new Student { Id = 1, Name = "John Doe", Email = "john.doe@example.com" },
            new Student { Id = 2, Name = "Jane Smith", Email = "jane.smith@example.com" },
            new Student { Id = 3, Name = "Nguyen Van Hung", Email = "vanhungdev05@gmai.com"}
        );
        }
    }
}

using Microsoft.EntityFrameworkCore;
using TodoAssignment.Models;

namespace TodoAssignment.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>()
                .HasKey(t => t.Id); 

            modelBuilder.Entity<ToDo>()
                .Property(t => t.Id)
                .ValueGeneratedOnAdd(); 
        }
    }
}

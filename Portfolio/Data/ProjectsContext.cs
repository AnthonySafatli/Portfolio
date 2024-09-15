using Portfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Data;

public class ProjectsContext : DbContext
{
    public DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data source=./Data/Projects.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>().HasKey(t => t.Name);
    }
}

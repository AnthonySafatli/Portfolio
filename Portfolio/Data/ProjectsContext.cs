using Portfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Data;

public class ProjectsContext : DbContext
{
    public DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data source=Projects.db");
    }
}

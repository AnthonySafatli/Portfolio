using Portfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Data;

public class ProjectsContext : DbContext
{
    public ProjectsContext(DbContextOptions<ProjectsContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<TechStackItem> TechStackItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>()
            .HasMany(p => p.TechStackItems)
            .WithMany(t => t.Projects)
            .UsingEntity(j => j.ToTable("ProjectTechStack"));
    }
}

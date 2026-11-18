using Microsoft.EntityFrameworkCore;
using TeamLinkHub.Models;

namespace TeamLinkHub.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ToolLink> ToolLinks => Set<ToolLink>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ToolLink>(entity =>
        {
            entity.HasIndex(x => x.Category);
            entity.HasIndex(x => x.Status);
            entity.Property(x => x.Name).HasMaxLength(120);
            entity.Property(x => x.Url).HasMaxLength(2048);
            entity.Property(x => x.Category).HasMaxLength(80);
        });
    }
}

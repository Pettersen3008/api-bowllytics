using Bowllytics.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bowllytics.Data;

public class BowlsDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public BowlsDbContext(DbContextOptions<BowlsDbContext> options) : base(options) { }

    public DbSet<Match> Matches { get; set; }
    public DbSet<End> Ends { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Important to keep Identity configuration
        
        modelBuilder.HasPostgresEnum<MatchStatus>();
        
        // ✅ Match → Ends (One-to-Many)
        modelBuilder.Entity<Match>()
            .HasMany(m => m.Ends)
            .WithOne(e => e.Match)
            .HasForeignKey(e => e.MatchId)
            .OnDelete(DeleteBehavior.Cascade);

        // ✅ Match → CreatedByUser (Many-to-One)
        modelBuilder.Entity<Match>()
            .HasOne(m => m.CreatedByUser)
            .WithMany()
            .HasForeignKey(m => m.CreatedBy)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PAS.Web.Models;

namespace PAS.Web.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ResearchArea> ResearchAreas => Set<ResearchArea>();
    public DbSet<ProjectProposal> ProjectProposals => Set<ProjectProposal>();
    public DbSet<SupervisorExpertise> SupervisorExpertise => Set<SupervisorExpertise>();
    public DbSet<Match> Matches => Set<Match>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ResearchArea>()
            .HasIndex(r => r.Name)
            .IsUnique();

        builder.Entity<ProjectProposal>()
            .HasOne(p => p.Student)
            .WithMany()
            .HasForeignKey(p => p.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SupervisorExpertise>()
            .HasOne(se => se.Supervisor)
            .WithMany()
            .HasForeignKey(se => se.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SupervisorExpertise>()
            .HasOne(se => se.ResearchArea)
            .WithMany()
            .HasForeignKey(se => se.ResearchAreaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Match>()
            .HasOne(m => m.ProjectProposal)
            .WithMany()
            .HasForeignKey(m => m.ProjectProposalId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Match>()
            .HasOne(m => m.Supervisor)
            .WithMany()
            .HasForeignKey(m => m.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
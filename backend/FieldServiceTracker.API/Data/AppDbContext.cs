using FieldServiceTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FieldServiceTracker.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<ServiceRequest> ServiceRequests => Set<ServiceRequest>();
    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<ServiceRequestAuditLog> ServiceRequestAuditLogs => Set<ServiceRequestAuditLog>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ServiceRequest>()
            .HasIndex(x => x.TicketNumber)
            .IsUnique();

        modelBuilder.Entity<ServiceRequest>()
            .Property(x => x.CustomerName)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<ServiceRequest>()
            .Property(x => x.Location)
            .HasMaxLength(150)
            .IsRequired();

        modelBuilder.Entity<ServiceRequest>()
            .Property(x => x.Priority)
            .HasMaxLength(20)
            .IsRequired();

        modelBuilder.Entity<ServiceRequest>()
            .Property(x => x.Status)
            .HasMaxLength(20)
            .IsRequired();

        modelBuilder.Entity<ServiceRequest>()
            .Property(x => x.AssignedTechnician)
            .HasMaxLength(100);

        // Audit log configuration

        modelBuilder.Entity<ServiceRequestAuditLog>()
            .Property(x => x.TicketNumber)
            .HasMaxLength(50)
            .IsRequired();

        modelBuilder.Entity<ServiceRequestAuditLog>()
            .Property(x => x.Action)
            .HasMaxLength(50)
            .IsRequired();


        modelBuilder.Entity<ServiceRequestAuditLog>()
            .Property(x => x.Details)
            .HasMaxLength(1000)
            .IsRequired();

        modelBuilder.Entity<AppUser>()
            .HasIndex(x => x.Email)
            .IsUnique();

        modelBuilder.Entity<AppUser>()
            .Property(x => x.FullName)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<AppUser>()
            .Property(x => x.Email)
            .HasMaxLength(150)
            .IsRequired();

        modelBuilder.Entity<AppUser>()
            .Property(x => x.PasswordHash)
            .IsRequired();

        modelBuilder.Entity<AppUser>()
            .Property(x => x.Role)
            .HasMaxLength(50)
            .IsRequired();
    }
}
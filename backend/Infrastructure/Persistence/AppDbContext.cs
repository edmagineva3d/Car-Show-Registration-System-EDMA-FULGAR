namespace Infrastructure.Persistence;

using Application.Abstractions.Data;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Implement each DbSet<TEntity> declared in IAppDbContext, e.g.
    // public DbSet<Student> Students => Set<Student>();
    public DbSet<Registration> Registrations => Set<Registration>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure keys, required fields, lengths, and relationships per entity
        

    // inside OnModelCreating(ModelBuilder modelBuilder):
    modelBuilder.Entity<Registration>(entity =>
{
    entity.HasKey(e => e.Id);

    entity.Property(e => e.RegistrationNumber).IsRequired().HasMaxLength(20);
    entity.Property(e => e.ParticipantName).IsRequired().HasMaxLength(150);
    entity.Property(e => e.ContactNumber).IsRequired().HasMaxLength(11);
    entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
    entity.Property(e => e.Address).IsRequired().HasMaxLength(300);
    entity.Property(e => e.CarMakeModel).IsRequired().HasMaxLength(100);
    entity.Property(e => e.PlateNumber).IsRequired().HasMaxLength(15);

    entity.Property(e => e.RegistrationType).HasConversion<string>().HasMaxLength(20);
    entity.Property(e => e.Status).HasConversion<string>().HasMaxLength(20);

    entity.Property(e => e.RegistrationFee).HasPrecision(18, 2);
    entity.Property(e => e.AmountPaid).HasPrecision(18, 2);

    // SQLite returns Kind=Unspecified, which would drop the "Z" from the JSON date
    entity.Property(e => e.RegistrationDate)
          .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

    entity.HasIndex(e => e.PlateNumber).IsUnique();
    entity.HasIndex(e => e.RegistrationNumber).IsUnique();

    entity.Ignore(e => e.PaymentStatus);
});
    }
}

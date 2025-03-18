using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HealthFitnessServer.DBModel;

public partial class HealthFitnessContext : DbContext
{
    public HealthFitnessContext()
    {
    }

    public HealthFitnessContext(DbContextOptions<HealthFitnessContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calory> Calories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WorkoutLog> WorkoutLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=LAPTOP-7OJP0J0D; integrated security=SSPI; database=HealthFitness;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calory>(entity =>
        {
            entity.HasKey(e => e.CalorieId).HasName("PK__Calories__B7B23B10276B3F45");

            entity.Property(e => e.FoodItem)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Calories)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Calories__UserId__49C3F6B7");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07135D06EF");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534A346C09C").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Mobile).HasMaxLength(20);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
        });

        modelBuilder.Entity<WorkoutLog>(entity =>
        {
            entity.HasKey(e => e.WorkoutId).HasName("PK__WorkoutL__E1C42A01ECD90E3D");

            entity.Property(e => e.WorkoutType)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.WorkoutLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__WorkoutLo__UserI__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

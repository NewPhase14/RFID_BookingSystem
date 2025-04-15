using System;
using System.Collections.Generic;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Postgres.Scaffolding;

public partial class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingStatus> BookingStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceTimeSlot> ServiceTimeSlots { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Weekday> Weekdays { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bookings_pkey");

            entity.ToTable("bookings");

            entity.HasIndex(e => new { e.SlotId, e.BookingDate }, "bookings_slot_id_booking_date_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookingDate).HasColumnName("booking_date");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.SlotId).HasColumnName("slot_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Service).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bookings_service_id_fkey");

            entity.HasOne(d => d.Slot).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.SlotId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bookings_slot_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("bookings_status_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("bookings_user_id_fkey");
        });

        modelBuilder.Entity<BookingStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("booking_status_pkey");

            entity.ToTable("booking_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("services_pkey");

            entity.ToTable("services");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ServiceTimeSlot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("service_time_slots_pkey");

            entity.ToTable("service_time_slots");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DayOfWeek).HasColumnName("day_of_week");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.StartTime).HasColumnName("start_time");

            entity.HasOne(d => d.DayOfWeekNavigation).WithMany(p => p.ServiceTimeSlots)
                .HasForeignKey(d => d.DayOfWeek)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("service_time_slots_day_of_week_fkey");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceTimeSlots)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("service_time_slots_service_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConfirmedEmail)
                .HasDefaultValue(false)
                .HasColumnName("confirmed_email");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.HashedPassword).HasColumnName("hashed_password");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.Rfid)
                .HasMaxLength(100)
                .HasColumnName("rfid");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Salt).HasColumnName("salt");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");
        });

        modelBuilder.Entity<Weekday>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("weekdays_pkey");

            entity.ToTable("weekdays");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APIV10.Models.ReservApp;

public partial class ReservAppContext : DbContext
{
    public ReservAppContext()
    {
    }

    public ReservAppContext(DbContextOptions<ReservAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddressInfo> AddressInfos { get; set; }

    public virtual DbSet<Amenity> Amenitys { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Host> Hosts { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PropertieAmenity> PropertieAmenitys { get; set; }

    public virtual DbSet<PropertiePhoto> PropertiePhotos { get; set; }

    public virtual DbSet<PropertiesInfo> PropertiesInfos { get; set; }

    public virtual DbSet<ReservationDatum> ReservationData { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddressInfo>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__AddressI__091C2A1BCD98A8FF");

            entity.ToTable("AddressInfo");

            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Area)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Street)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Amenity>(entity =>
        {
            entity.HasKey(e => e.AmenityId).HasName("PK__Amenitys__842AF52BDE85A60A");

            entity.Property(e => e.AmenityId).HasColumnName("AmenityID");
            entity.Property(e => e.Description)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B85446F8E0");

            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(8000);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Host>(entity =>
        {
            entity.HasKey(e => e.HostId).HasName("PK__Hosts__08D4870C8F7A83C5");

            entity.Property(e => e.HostId).HasColumnName("HostID");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash).HasMaxLength(8000);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58D09ACF66");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Payment1)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Payment");
            entity.Property(e => e.ReservId).HasColumnName("ReservID");

            entity.HasOne(d => d.Reserv).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ReservId)
                .HasConstraintName("FK_Payments_ReservID");
        });

        modelBuilder.Entity<PropertieAmenity>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.AmenityId).HasColumnName("AmenityID");
            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");

            entity.HasOne(d => d.Amenity).WithMany()
                .HasForeignKey(d => d.AmenityId)
                .HasConstraintName("FK_PropertieAmenitys_AmenityID");

            entity.HasOne(d => d.Place).WithMany()
                .HasForeignKey(d => d.PlaceId)
                .HasConstraintName("FK_PropertieAmenitys_PlaceID");
        });

        modelBuilder.Entity<PropertiePhoto>(entity =>
        {
            entity.HasKey(e => e.PhotoId).HasName("PK__Properti__21B7B582BCED6BCD");

            entity.Property(e => e.PhotoId).HasColumnName("PhotoID");
            entity.Property(e => e.PhotoUrl)
                .IsUnicode(false)
                .HasColumnName("PhotoURL");
            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");

            entity.HasOne(d => d.Place).WithMany(p => p.PropertiePhotos)
                .HasForeignKey(d => d.PlaceId)
                .HasConstraintName("FK_PropertiePhotos_PlaceID");
        });

        modelBuilder.Entity<PropertiesInfo>(entity =>
        {
            entity.HasKey(e => e.PlaceId).HasName("PK__Properti__D5222B4EF95F1654");

            entity.ToTable("PropertiesInfo");

            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");
            entity.Property(e => e.AddressId).HasColumnName("AddressID");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HostId).HasColumnName("HostID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Address).WithMany(p => p.PropertiesInfos)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_PropertiesInfo_AddressID");

            entity.HasOne(d => d.Host).WithMany(p => p.PropertiesInfos)
                .HasForeignKey(d => d.HostId)
                .HasConstraintName("FK_PropertiesInfo_HostID");
        });

        modelBuilder.Entity<ReservationDatum>(entity =>
        {
            entity.HasKey(e => e.ReservId).HasName("PK__Reservat__362B3376B76934D7");

            entity.Property(e => e.ReservId).HasColumnName("ReservID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.PlaceId).HasColumnName("PlaceID");

            entity.HasOne(d => d.Customer).WithMany(p => p.ReservationData)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_ReservationData_CustomerID");

            entity.HasOne(d => d.Place).WithMany(p => p.ReservationData)
                .HasForeignKey(d => d.PlaceId)
                .HasConstraintName("FK_ReservationData_PlaceID");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AE9681F2FB");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.Comment).IsUnicode(false);
            entity.Property(e => e.DateReview).HasColumnType("datetime");
            entity.Property(e => e.ReservId).HasColumnName("ReservID");

            entity.HasOne(d => d.Reserv).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ReservId)
                .HasConstraintName("FK_Reviews_ReservID");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Sales__712CC627E9CFD6D8");

            entity.Property(e => e.TicketId).HasColumnName("TicketID");
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");

            entity.HasOne(d => d.Payment).WithMany(p => p.Sales)
                .HasForeignKey(d => d.PaymentId)
                .HasConstraintName("FK_Sales_PaymentID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

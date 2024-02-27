using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.BankingManagementSystem.Database.EfAppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAccount> TblAccounts { get; set; }

    public virtual DbSet<TblAdminUser> TblAdminUsers { get; set; }

    public virtual DbSet<TblPlaceState> TblPlaceStates { get; set; }

    public virtual DbSet<TblPlaceTownship> TblPlaceTownships { get; set; }

    public virtual DbSet<TblTransactionHistory> TblTransactionHistories { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK_Account");

            entity.ToTable("Tbl_Account");

            entity.Property(e => e.AccountNo).HasMaxLength(50);
            entity.Property(e => e.AccountType).HasMaxLength(50);
            entity.Property(e => e.Balance).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.CustomerCode).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(255);
        });

        modelBuilder.Entity<TblAdminUser>(entity =>
        {
            entity.HasKey(e => e.AdminUserId).HasName("PK_Tbl_Employee");

            entity.ToTable("Tbl_AdminUser");

            entity.Property(e => e.AdminUserCode).HasMaxLength(50);
            entity.Property(e => e.AdminUserName).HasMaxLength(50);
            entity.Property(e => e.MobileNo).HasMaxLength(15);
            entity.Property(e => e.UserRoleCode).HasMaxLength(50);
        });

        modelBuilder.Entity<TblPlaceState>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK_Tbl_City");

            entity.ToTable("Tbl_PlaceState");

            entity.Property(e => e.StateCode).HasMaxLength(50);
            entity.Property(e => e.StateName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblPlaceTownship>(entity =>
        {
            entity.HasKey(e => e.TownshipId).HasName("PK_Tbl_Township");

            entity.ToTable("Tbl_PlaceTownship");

            entity.Property(e => e.StateCode).HasMaxLength(50);
            entity.Property(e => e.TownshipCode).HasMaxLength(50);
            entity.Property(e => e.TownshipName).HasMaxLength(50);
        });

        modelBuilder.Entity<TblTransactionHistory>(entity =>
        {
            entity.HasKey(e => e.TransactionHistoryId).HasName("PK_Tbl_Transfer");

            entity.ToTable("Tbl_TransactionHistory");

            entity.Property(e => e.AdminUserCode).HasMaxLength(50);
            entity.Property(e => e.Amount).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.FromAccountNo).HasMaxLength(50);
            entity.Property(e => e.ToAccountNo).HasMaxLength(50);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_Customer");

            entity.ToTable("Tbl_User");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.MobileNo).HasMaxLength(15);
            entity.Property(e => e.Nrc).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.StateCode).HasMaxLength(50);
            entity.Property(e => e.TownshipCode).HasMaxLength(50);
            entity.Property(e => e.UserCode).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

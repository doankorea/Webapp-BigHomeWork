using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NewBTL.Models;

namespace NewBTL.Models;

public partial class QldienthoaiContext : DbContext
{
    public QldienthoaiContext()
    {
    }

    public QldienthoaiContext(DbContextOptions<QldienthoaiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chitietdonhang> Chitietdonhangs { get; set; }

    public virtual DbSet<Donhang> Donhangs { get; set; }

    public virtual DbSet<Hangsanxuat> Hangsanxuats { get; set; }

    public virtual DbSet<Hedieuhanh> Hedieuhanhs { get; set; }

    public virtual DbSet<Nguoidung> Nguoidungs { get; set; }

    public virtual DbSet<PhanQuyen> PhanQuyens { get; set; }

    public virtual DbSet<Sanpham> Sanphams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=DUONGTU\\DUC;Initial Catalog=QLdienthoai;User ID=sa;Password=123;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chitietdonhang>(entity =>
        {
            entity.HasKey(e => new { e.Madon, e.Masp });

            entity.ToTable("Chitietdonhang");

            entity.Property(e => e.Dongia).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Thanhtien).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.MadonNavigation).WithMany(p => p.Chitietdonhangs)
                .HasForeignKey(d => d.Madon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chitietdonhang_Donhang");

            entity.HasOne(d => d.MaspNavigation).WithMany(p => p.Chitietdonhangs)
                .HasForeignKey(d => d.Masp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chitietdonhang_Sanpham");
        });

        modelBuilder.Entity<Donhang>(entity =>
        {
            entity.HasKey(e => e.Madon);

            entity.ToTable("Donhang");

            entity.Property(e => e.Diachinhanhang).HasMaxLength(100);
            entity.Property(e => e.Ngaydat).HasColumnType("datetime");
            entity.Property(e => e.Tongtien).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.MaNguoidungNavigation).WithMany(p => p.Donhangs)
                .HasForeignKey(d => d.MaNguoidung)
                .HasConstraintName("FK_Donhang_Khachhang");
        });

        modelBuilder.Entity<Hangsanxuat>(entity =>
        {
            entity.HasKey(e => e.Mahang);

            entity.ToTable("Hangsanxuat");

            entity.Property(e => e.Tenhang)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Hedieuhanh>(entity =>
        {
            entity.HasKey(e => e.Mahdh);

            entity.ToTable("Hedieuhanh");

            entity.Property(e => e.Tenhdh)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Nguoidung>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PK_Khachhang");

            entity.ToTable("Nguoidung");

            entity.Property(e => e.Anhdaidien)
                .HasMaxLength(30)
                .IsFixedLength();
            entity.Property(e => e.Diachi).HasMaxLength(100);
            entity.Property(e => e.Dienthoai)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Hoten).HasMaxLength(50);
            entity.Property(e => e.Idquyen)
                .HasDefaultValueSql("((0))")
                .HasColumnName("IDQuyen");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdquyenNavigation).WithMany(p => p.Nguoidungs)
                .HasForeignKey(d => d.Idquyen)
                .HasConstraintName("FK_Nguoidung_Nguoidung");
        });

        modelBuilder.Entity<PhanQuyen>(entity =>
        {
            entity.HasKey(e => e.Idquyen);

            entity.ToTable("PhanQuyen");

            entity.Property(e => e.Idquyen).HasColumnName("IDQuyen");
            entity.Property(e => e.TenQuyen).HasMaxLength(20);
        });

        modelBuilder.Entity<Sanpham>(entity =>
        {
            entity.HasKey(e => e.Masp);

            entity.ToTable("Sanpham");

            entity.Property(e => e.Anhbia).HasMaxLength(50);
            entity.Property(e => e.Giatien).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Mota).HasColumnType("ntext");
            entity.Property(e => e.Tensp).HasMaxLength(50);

            entity.HasOne(d => d.MahangNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Mahang)
                .HasConstraintName("FK_Sanpham_Hangsanxuat");

            entity.HasOne(d => d.MahdhNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.Mahdh)
                .HasConstraintName("FK_Sanpham_Hedieuhanh");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<NewBTL.Models.Thongke>? Thongke { get; set; }
}

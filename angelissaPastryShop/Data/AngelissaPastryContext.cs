using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace angelissaPastryShop.Data;

public partial class AngelissaPastryContext : DbContext
{
    public AngelissaPastryContext()
    {
    }

    public AngelissaPastryContext(DbContextOptions<AngelissaPastryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<DetallesOrden> DetallesOrdens { get; set; }

    public virtual DbSet<Orden> Ordenes { get; set; }

    public virtual DbSet<PreciosProducto> PreciosProductos { get; set; }

    public virtual DbSet<Presentacion> Presentaciones { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaID).HasName("PK__Categori__F353C1C577589C5C");

            entity.HasIndex(e => e.Nombre, "UQ__Categori__75E3EFCF039E3FFE").IsUnique();

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<DetallesOrden>(entity =>
        {
            entity.HasKey(e => e.DetalleID).HasName("PK__Detalles__6E19D6FAAE5E781D");

            entity.ToTable("DetallesOrden");

            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([Cantidad]*[PrecioUnitario])", true)
                .HasColumnType("decimal(21, 2)");

            entity.HasOne(d => d.Orden).WithMany(p => p.DetallesOrdens)
                .HasForeignKey(d => d.OrdenID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallesO__Orden__35BCFE0A");

            entity.HasOne(d => d.Presentacion).WithMany(p => p.DetallesOrdens)
                .HasForeignKey(d => d.PresentacionID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallesO__Prese__37A5467C");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetallesOrdens)
                .HasForeignKey(d => d.ProductoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallesO__Produ__36B12243");
        });

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.HasKey(e => e.OrdenID).HasName("PK__Ordenes__C088A4E4BB2CCD30");

            entity.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<PreciosProducto>(entity =>
        {
            entity.HasKey(e => e.PrecioID).HasName("PK__PreciosP__061E6574640D3C50");

            entity.ToTable("PreciosProducto");

            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Presentacion).WithMany(p => p.PreciosProductos)
                .HasForeignKey(d => d.PresentacionID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PreciosPr__Prese__2E1BDC42");

            entity.HasOne(d => d.Producto).WithMany(p => p.PreciosProductos)
                .HasForeignKey(d => d.ProductoID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PreciosPr__Produ__2D27B809");
        });

        modelBuilder.Entity<Presentacion>(entity =>
        {
            entity.HasKey(e => e.PresentacionID).HasName("PK__Presenta__F3D9E301846B2C36");

            entity.HasIndex(e => e.Nombre, "UQ__Presenta__75E3EFCFA58AD53C").IsUnique();

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoID).HasName("PK__Producto__A430AE833F9D8E54");

            entity.Property(e => e.Imagen).HasMaxLength(255);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__Categ__276EDEB3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace angelissaPastryShop.Models;

public partial class AngelissaShopDbv0Context : DbContext
{
    public AngelissaShopDbv0Context()
    {
    }

    public AngelissaShopDbv0Context(DbContextOptions<AngelissaShopDbv0Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetallesOrden> DetallesOrdens { get; set; }

    public virtual DbSet<Orden> Ordenes { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<PreciosProducto> PreciosProductos { get; set; }

    public virtual DbSet<Presentacione> Presentaciones { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Sucursale> Sucursales { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__Categori__F353C1C5AB1E1622");

            entity.HasIndex(e => e.Nombre, "UQ__Categori__75E3EFCF98C479AD").IsUnique();

            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__Clientes__71ABD0A777C75001");

            entity.HasIndex(e => e.Correo, "UQ__Clientes__60695A19F5A2D5FD").IsUnique();

            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Correo).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Telefono).HasMaxLength(20);
        });

        modelBuilder.Entity<DetallesOrden>(entity =>
        {
            entity.HasKey(e => e.DetalleId).HasName("PK__Detalles__6E19D6FA971C5E9B");

            entity.ToTable("DetallesOrden");

            entity.Property(e => e.DetalleId).HasColumnName("DetalleID");
            entity.Property(e => e.OrdenId).HasColumnName("OrdenID");
            entity.Property(e => e.PrecioUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PresentacionId).HasColumnName("PresentacionID");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.Subtotal)
                .HasComputedColumnSql("([Cantidad]*[PrecioUnitario])", true)
                .HasColumnType("decimal(21, 2)");

            entity.HasOne(d => d.Orden).WithMany(p => p.DetallesOrdens)
                .HasForeignKey(d => d.OrdenId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallesO__Orden__35BCFE0A");

            entity.HasOne(d => d.Presentacion).WithMany(p => p.DetallesOrdens)
                .HasForeignKey(d => d.PresentacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallesO__Prese__37A5467C");

            entity.HasOne(d => d.Producto).WithMany(p => p.DetallesOrdens)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetallesO__Produ__36B12243");
        });      

        modelBuilder.Entity<Orden>(entity =>
        {
            entity.HasKey(e => e.OrdenId).HasName("PK__Ordenes__C088A4E401E5295D");

            entity.Property(e => e.OrdenId).HasColumnName("OrdenID");
            entity.Property(e => e.ClienteId).HasColumnName("ClienteID");
            entity.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SucursalId).HasColumnName("SucursalID");
            entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("FK_Ordenes_Clientes");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.SucursalId)
                .HasConstraintName("FK_Ordenes_Sucursales");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Ordenes_Usuarios");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.PagoId).HasName("PK__Pagos__F00B615882DAD7EB");

            entity.Property(e => e.PagoId).HasColumnName("PagoID");
            entity.Property(e => e.FechaPago)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MetodoPago)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.OrdenId).HasColumnName("OrdenID");

            entity.HasOne(d => d.Orden).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.OrdenId)
                .HasConstraintName("FK_Pagos_Ordenes");
        });

        modelBuilder.Entity<PreciosProducto>(entity =>
        {
            entity.HasKey(e => e.PrecioId).HasName("PK__PreciosP__061E65740D019B04");

            entity.ToTable("PreciosProducto");

            entity.Property(e => e.PrecioId).HasColumnName("PrecioID");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PresentacionId).HasColumnName("PresentacionID");
            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");

            entity.HasOne(d => d.Presentacion).WithMany(p => p.PreciosProductos)
                .HasForeignKey(d => d.PresentacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PreciosPr__Prese__2E1BDC42");

            entity.HasOne(d => d.Producto).WithMany(p => p.PreciosProductos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PreciosPr__Produ__2D27B809");
        });

        modelBuilder.Entity<Presentacione>(entity =>
        {
            entity.HasKey(e => e.PresentacionId).HasName("PK__Presenta__F3D9E3010389F4D6");

            entity.HasIndex(e => e.Nombre, "UQ__Presenta__75E3EFCF0E7C7B46").IsUnique();

            entity.Property(e => e.PresentacionId).HasColumnName("PresentacionID");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__Producto__A430AE83F2DC5471");

            entity.Property(e => e.ProductoId).HasColumnName("ProductoID");
            entity.Property(e => e.CategoriaId).HasColumnName("CategoriaID");
            entity.Property(e => e.Imagen).HasMaxLength(255);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productos__Categ__276EDEB3");
        });

        modelBuilder.Entity<Sucursale>(entity =>
        {
            entity.HasKey(e => e.SucursalId).HasName("PK__Sucursal__6CB48281160EAE82");

            entity.Property(e => e.SucursalId).HasColumnName("SucursalID");
            entity.Property(e => e.Direccion)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798343A2D4A");

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuarios__6B0F5AE092A2E2D3").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.NombreUsuario)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Pin)
                .IsRequired()
                .HasMaxLength(4)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PIN");
            entity.Property(e => e.Rol)
                .IsRequired()
                .HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

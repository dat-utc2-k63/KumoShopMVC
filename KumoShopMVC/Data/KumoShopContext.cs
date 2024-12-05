using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KumoShopMVC.Data;

public partial class KumoShopContext : DbContext
{
    public KumoShopContext()
    {
    }

    public KumoShopContext(DbContextOptions<KumoShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Favourite> Favourites { get; set; }

    public virtual DbSet<FavouriteItem> FavouriteItems { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentMethode> PaymentMethodes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductColor> ProductColors { get; set; }

    public virtual DbSet<ProductDetailsView> ProductDetailsViews { get; set; }

    public virtual DbSet<ProductSize> ProductSizes { get; set; }

    public virtual DbSet<RatingImage> RatingImages { get; set; }

    public virtual DbSet<RatingProduct> RatingProducts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<StatusShipping> StatusShippings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-98U3CSGC\\MATERMOS;Initial Catalog=KumoShop;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.NameCategory).HasMaxLength(50);
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.ToTable("Color");

            entity.Property(e => e.ColorName).HasMaxLength(20);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContactId).HasName("PK_Feedback");

            entity.ToTable("Contact");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DescContact).HasColumnType("ntext");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Subject).HasMaxLength(50);
        });

        modelBuilder.Entity<Favourite>(entity =>
        {
            entity.ToTable("Favourite");

            entity.Property(e => e.DescFavourite).HasColumnType("ntext");

            entity.HasOne(d => d.User).WithMany(p => p.Favourites)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favourite_Users");
        });

        modelBuilder.Entity<FavouriteItem>(entity =>
        {
            entity.ToTable("FavouriteItem");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Favourite).WithMany(p => p.FavouriteItems)
                .HasForeignKey(d => d.FavouriteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FavouriteItem_Favourite");

            entity.HasOne(d => d.Product).WithMany(p => p.FavouriteItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FavouriteItem_Products");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("Image");

            entity.Property(e => e.ImageUrl).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.Images)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Image_Products");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_orders");

            entity.HasIndex(e => e.StatusId, "IX_Orders_StatusId");

            entity.HasIndex(e => e.UserId, "IX_Orders_UserId");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.DescOrder).HasColumnType("ntext");
            entity.Property(e => e.Fullname).HasMaxLength(50);
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethode).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.ShippingDate).HasColumnType("datetime");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Orders_StatusShipping");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Users");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK_order_item");

            entity.ToTable("OrderItem");

            entity.HasIndex(e => e.OrderId, "IX_OrderItem_OrderId");

            entity.HasIndex(e => e.ProductId, "IX_OrderItem_ProductId");

            entity.Property(e => e.Color).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.NameProduct).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_item_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_item_Products");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK_payment_method");

            entity.ToTable("Payment");

            entity.HasIndex(e => e.UserId, "IX_PaymentInformation_UserId");

            entity.Property(e => e.PaymentId).HasMaxLength(50);
            entity.Property(e => e.CardHolder).HasMaxLength(50);
            entity.Property(e => e.CardNumber).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Cvv)
                .HasMaxLength(50)
                .HasColumnName("CVV");
            entity.Property(e => e.ExpDate).HasMaxLength(50);
            entity.Property(e => e.FullNameCard)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Quantity).HasMaxLength(50);

            entity.HasOne(d => d.PaymentMethode).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PaymentMethodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentInformation_PaymentMethod");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_information_Users");
        });

        modelBuilder.Entity<PaymentMethode>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodeId).HasName("PK_Payment_method_1");

            entity.ToTable("PaymentMethode");

            entity.Property(e => e.NamePaymentMethode).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Brands).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DescProduct).HasColumnType("ntext");
            entity.Property(e => e.NameProduct).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Categories");
        });

        modelBuilder.Entity<ProductColor>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductColor");

            entity.HasOne(d => d.Color).WithMany()
                .HasForeignKey(d => d.ColorId)
                .HasConstraintName("FK_ProductColor_Color1");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductColor_Products");
        });

        modelBuilder.Entity<ProductDetailsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProductDetailsView");

            entity.Property(e => e.Brands).HasMaxLength(50);
            entity.Property(e => e.ColorName).HasMaxLength(20);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DescProduct).HasColumnType("ntext");
            entity.Property(e => e.ImageUrl).HasMaxLength(50);
            entity.Property(e => e.NameProduct).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductSize>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ProductSize");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_ProductSize_Products");

            entity.HasOne(d => d.Size).WithMany()
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK_ProductSize_Size1");
        });

        modelBuilder.Entity<RatingImage>(entity =>
        {
            entity.ToTable("RatingImage");

            entity.Property(e => e.RatingImageId).ValueGeneratedNever();
            entity.Property(e => e.ImageUrl).HasMaxLength(50);

            entity.HasOne(d => d.Rating).WithMany(p => p.RatingImages)
                .HasForeignKey(d => d.RatingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RatingImage_RatingProduct");
        });

        modelBuilder.Entity<RatingProduct>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK_Rating_product");

            entity.ToTable("RatingProduct");

            entity.HasIndex(e => e.ProductId, "IX_RatingProduct_ProductId");

            entity.HasIndex(e => e.UserId, "IX_RatingProduct_UserId");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DescRating).HasColumnType("ntext");
            entity.Property(e => e.Fullname).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.RatingProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rating_product_Products");

            entity.HasOne(d => d.User).WithMany(p => p.RatingProducts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Rating_product_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_roles");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.NameRole).HasMaxLength(50);
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.ToTable("Size");
        });

        modelBuilder.Entity<StatusShipping>(entity =>
        {
            entity.HasKey(e => e.StatusId);

            entity.ToTable("StatusShipping");

            entity.Property(e => e.StatusId).ValueGeneratedNever();
            entity.Property(e => e.DescShipping).HasMaxLength(50);
            entity.Property(e => e.NameStatus).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_users");

            entity.Property(e => e.AboutUs).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Avatar).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Fullname).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.RandomKey).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

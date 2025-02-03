using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace edok_kiosk.Model;

public partial class EdokContext : DbContext
{
    public EdokContext()
    {
    }

    public EdokContext(DbContextOptions<EdokContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<FoodCategory> FoodCategories { get; set; }

    public virtual DbSet<KitchenStatus> KitchenStatuses { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Moderator> Moderators { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<RestaurantCategory> RestaurantCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=1234;database=edok", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.39-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cart");

            entity.HasIndex(e => e.RestaurantId, "fk_cart_restaurant_id_idx");

            entity.HasIndex(e => e.UserId, "fk_cart_user_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'active'")
                .HasColumnType("enum('active','ordered')")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Carts)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cart_restaurant_id");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cart_user_id");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cart_item");

            entity.HasIndex(e => e.CartId, "fk_ci_cart_id_idx");

            entity.HasIndex(e => e.FoodId, "fk_ci_food_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.FoodQuantity)
                .HasDefaultValueSql("'1'")
                .HasColumnName("food_quantity");
            entity.Property(e => e.UtensilsQuantity)
                .HasDefaultValueSql("'1'")
                .HasColumnName("utensils_quantity");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ci_cart_id");

            entity.HasOne(d => d.Food).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ci_food_id");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("category");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("food");

            entity.HasIndex(e => e.FoodCategoryId, "fk_food_food_category_id_idx");

            entity.HasIndex(e => e.RestaurantId, "fk_food_restaurant_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Disabled).HasColumnName("disabled");
            entity.Property(e => e.FoodCategoryId).HasColumnName("food_category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Photo).HasColumnName("photo");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

            entity.HasOne(d => d.FoodCategory).WithMany(p => p.Foods)
                .HasForeignKey(d => d.FoodCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_food_food_category_id");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Foods)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_food_restaurant_id");
        });

        modelBuilder.Entity<FoodCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("food_category");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<KitchenStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("kitchen_status");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("manager");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Inn, "inn_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Login, "login_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Passport, "passport_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Inn)
                .HasMaxLength(12)
                .HasColumnName("inn");
            entity.Property(e => e.Login)
                .HasMaxLength(20)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Passport)
                .HasMaxLength(10)
                .HasColumnName("passport");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(45)
                .HasColumnName("patronymic");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Moderator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("moderator");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Login, "login_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Login)
                .HasMaxLength(20)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(45)
                .HasColumnName("patronymic");
            entity.Property(e => e.Pfp).HasColumnName("pfp");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .HasColumnName("surname");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.CartId, "fk_order_cart_id_idx");

            entity.HasIndex(e => e.KitchenStatusId, "fk_order_ks_id_idx");

            entity.HasIndex(e => e.RestaurantId, "fk_order_restaurant_id_idx");

            entity.HasIndex(e => e.UserId, "fk_order_user_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .HasColumnName("address");
            entity.Property(e => e.CancelledAt)
                .HasColumnType("datetime")
                .HasColumnName("cancelled_at");
            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeliveredAt)
                .HasColumnType("datetime")
                .HasColumnName("delivered_at");
            entity.Property(e => e.KitchenStatusId)
                .HasDefaultValueSql("'1'")
                .HasColumnName("kitchen_status_id");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'active'")
                .HasColumnType("enum('active','finished','cancelled')")
                .HasColumnName("status");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Cart).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_cart_id");

            entity.HasOne(d => d.KitchenStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.KitchenStatusId)
                .HasConstraintName("fk_order_ks_id");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Orders)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_restaurant_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_order_user_id");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("restaurant");

            entity.HasIndex(e => e.ManagerId, "fk_restaurant_manager_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .HasColumnName("address");
            entity.Property(e => e.ClosingTime)
                .HasColumnType("time")
                .HasColumnName("closing_time");
            entity.Property(e => e.DisableReason)
                .HasMaxLength(500)
                .HasColumnName("disable_reason");
            entity.Property(e => e.Disabled).HasColumnName("disabled");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.Inn)
                .HasMaxLength(10)
                .HasColumnName("inn");
            entity.Property(e => e.LegalAddress)
                .HasMaxLength(150)
                .HasColumnName("legal_address");
            entity.Property(e => e.LegalInfo)
                .HasMaxLength(500)
                .HasColumnName("legal_info");
            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.OpeningTime)
                .HasColumnType("time")
                .HasColumnName("opening_time");
            entity.Property(e => e.Pending).HasColumnName("pending");
            entity.Property(e => e.RejectReason)
                .HasMaxLength(500)
                .HasColumnName("reject_reason");

            entity.HasOne(d => d.Manager).WithMany(p => p.Restaurants)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_restaurant_manager_id");
        });

        modelBuilder.Entity<RestaurantCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("restaurant_category");

            entity.HasIndex(e => e.CategoryId, "fk_rc_category_id_idx");

            entity.HasIndex(e => e.RestaurantId, "fk_rc_restaurant_id_idx");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.RestaurantId).HasColumnName("restaurant_id");

            entity.HasOne(d => d.Category).WithMany(p => p.RestaurantCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rc_category_id");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.RestaurantCategories)
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_rc_restaurant_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Id, "id_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Login, "login_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.LastAddress)
                .HasMaxLength(250)
                .HasColumnName("last_address");
            entity.Property(e => e.Login)
                .HasMaxLength(20)
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .HasColumnName("password");
            entity.Property(e => e.Pfp).HasColumnName("pfp");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

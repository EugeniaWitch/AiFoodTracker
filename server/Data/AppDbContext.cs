using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users => Set<User>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<UserSettings> UserSettings => Set<UserSettings>();
    public DbSet<NutritionGoal> NutritionGoals => Set<NutritionGoal>();
    public DbSet<WeightEntry> WeightEntries => Set<WeightEntry>();
    public DbSet<ProductCategory> ProductCategories => Set<ProductCategory>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductStock> ProductStocks => Set<ProductStock>();
    public DbSet<FoodEntry> FoodEntries => Set<FoodEntry>();
    public DbSet<DrinkEntry> DrinkEntries => Set<DrinkEntry>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
           entity.HasIndex(u => u.Email).IsUnique();
           entity.HasOne(u => u.Profile).WithOne(p => p.User)
           .HasForeignKey<UserProfile>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
           entity.HasOne(u => u.Settings).WithOne(s => s.User)
           .HasForeignKey<UserSettings>(s => s.UserId).OnDelete(DeleteBehavior.Cascade);
           entity.HasOne(u => u.NutritionGoal).WithOne(g => g.User)
           .HasForeignKey<NutritionGoal>(g => g.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<WeightEntry>(entity =>
        {
            entity.HasIndex(w => new {w.UserId, w.Date}).IsUnique();
            entity.HasOne(w => w.User).WithMany(u => u.WeightEntries)
            .HasForeignKey(w => w.UserId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasIndex(c => new {c.Type, c.Name}).IsUnique();
            entity.HasMany(c => c.Products).WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasIndex(p => p.Type);
            entity.HasIndex(p => p.CategoryId);
            entity.HasIndex(p => p.OwnerId);
            entity.HasOne(p => p.Owner).WithMany( u => u.Products)
            .HasForeignKey(p => p.OwnerId).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<ProductStock>(entity =>
        {
            entity.HasIndex(s => s.UserId);
            entity.HasIndex(s => s.ProductId);
            entity.HasOne(s => s.User).WithMany(u => u.ProductStocks)
            .HasForeignKey(s => s.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(s => s.Product).WithMany(p => p.ProductStocks)
            .HasForeignKey(s => s.ProductId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<FoodEntry>(entity =>
        {
           entity.HasIndex(e => new {e.UserId, e.Date});
           entity.HasIndex(e => e.ProductId);
           entity.HasIndex(e => e.ProductStockId);
           entity.HasOne(e => e.User).WithMany(u => u.FoodEntries)
           .HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
           entity.HasOne(e => e.Product).WithMany(p => p.FoodEntries)
           .HasForeignKey(e => e.ProductId).OnDelete(DeleteBehavior.Restrict);
           entity.HasOne(e => e.ProductStock).WithMany(s => s.FoodEntries)
           .HasForeignKey(e => e.ProductStockId).OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<DrinkEntry>(entity =>
        {
            entity.HasIndex(e => new{e.UserId, e.Date});
            entity.HasIndex(e => e.ProductId);
            entity.HasIndex(e => e.ProductStockId);
            entity.HasOne(e => e.User).WithMany(u => u.DrinkEntries)
            .HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Product).WithMany(p => p.DrinkEntries)
            .HasForeignKey(e => e.ProductId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e =>  e.ProductStock).WithMany(s => s.DrinkEntries)
            .HasForeignKey(e => e.ProductStockId).OnDelete(DeleteBehavior.SetNull);
        });
    }
}
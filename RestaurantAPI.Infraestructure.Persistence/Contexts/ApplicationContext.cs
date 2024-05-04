	using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Core.Domain.Entities;
using RestaurantAPI.Infraestructure.Persistence.Seeds;

namespace RestaurantAPI.Infraestructure.Persistence.Contexts
{
	public class ApplicationContext : DbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

		public DbSet<Ingredient> Ingredients { get; set; }
		public DbSet<IngredientDish> IngredientsDishes { get; set; }
		public DbSet<Dish> Dishes { get; set; }
		public DbSet<Table> Tables { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDish> OrdersDishes { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<TableStatus> TableStates { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			#region Seeds

			modelBuilder.ApplyConfiguration(new CategorySeed());
			modelBuilder.ApplyConfiguration(new TableStatusSeed()); 
	
			#endregion

			#region Tables

			modelBuilder.Entity<Ingredient>()
				.ToTable("Ingredients");

			modelBuilder.Entity<IngredientDish>()
				.ToTable("IngredientsDishes");

			modelBuilder.Entity<Dish>()
				.ToTable("Dishes");

			modelBuilder.Entity<Table>()
				.ToTable("Tables");

			modelBuilder.Entity<Order>()
				.ToTable("Orders");

			modelBuilder.Entity<OrderDish>()
				.ToTable("OrdersDishes");

			modelBuilder.Entity<Category>()
				.ToTable("Categories");

			modelBuilder.Entity<TableStatus>()
				.ToTable("TableStatus");

			#endregion

			#region Primary Keys

			modelBuilder.Entity<Ingredient>()
				.HasKey(x => x.ID);

			modelBuilder.Entity<Dish>()
				.HasKey(x => x.ID);

			modelBuilder.Entity<Table>()
				.HasKey(x => x.ID);

			modelBuilder.Entity<Order>()
				.HasKey(x => x.ID);

			modelBuilder.Entity<Category>()
				.HasKey(x => x.ID);

			modelBuilder.Entity<TableStatus>()
				.HasKey(x => x.ID);

			modelBuilder.Entity<OrderDish>()
				.HasKey(x => new { x.OrderID, x.DishID });

			modelBuilder.Entity<IngredientDish>()
				.HasKey(x => new { x.DishID, x.IngredientID });

			#endregion

			#region Relationships

			modelBuilder.Entity<Table>()
				.HasMany(t => t.Orders)
				.WithOne(o => o.Table)
				.HasForeignKey(o => o.TableID);

			modelBuilder.Entity<Category>()
				.HasMany(c => c.Dishes)
				.WithOne(d => d.Category)
				.HasForeignKey(d => d.CategoryID);

			modelBuilder.Entity<TableStatus>()
				.HasMany(c => c.Tables)
				.WithOne(d => d.Status)
				.HasForeignKey(d => d.StatusID);

			modelBuilder.Entity<OrderDish>()
				.HasOne(od => od.Order)
				.WithMany(o => o.OrderDishes)
				.HasForeignKey(od => od.OrderID);

			modelBuilder.Entity<OrderDish>()
				.HasOne(od => od.Dish)
				.WithMany(d => d.OrderDishes)
				.HasForeignKey(od => od.DishID);

			modelBuilder.Entity<IngredientDish>()
				.HasOne(id => id.Dish)
				.WithMany(d => d.IngredientDish)
				.HasForeignKey(id => id.DishID);

			modelBuilder.Entity<IngredientDish>()
				.HasOne(id => id.Ingredient)
				.WithMany(i => i.IngredientDish)
				.HasForeignKey(id => id.IngredientID);

			#endregion

			#region Property Configuration

			#region Ingredient

			modelBuilder.Entity<Ingredient>()
				.Property(x => x.Name)
				.IsRequired();

			#endregion

			#region Dish

			modelBuilder.Entity<Dish>()
				.Property(d => d.Name)
				.IsRequired();

			modelBuilder.Entity<Dish>()
				.Property(d => d.HowMany)
				.IsRequired();

			modelBuilder.Entity<Dish>()
				.Property(d => d.Price)
				.HasPrecision(30, 4)
				.IsRequired();

			modelBuilder.Entity<Dish>()
				.Property(d => d.CategoryID)
				.IsRequired();

			#endregion

			#region Table

			modelBuilder.Entity<Table>()
				.Property(t => t.PersonCapacity)
				.IsRequired();

			#endregion

			#region Category

			modelBuilder.Entity<Category>()
				.Property(c => c.Name)
				.IsRequired();

			#endregion

			#region Order

			modelBuilder.Entity<Order>()
				.Property(o => o.State)
				.IsRequired();

			modelBuilder.Entity<Order>()
				.Property(o => o.TableID)
				.IsRequired();

			modelBuilder.Entity<Order>()
				.Property(o => o.Subtotal)
				.HasPrecision(30, 4)
				.IsRequired();

			#endregion

			#endregion
		}

	}
}

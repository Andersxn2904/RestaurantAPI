using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace RestaurantAPI.Infraestructure.Identity.Contexts
{
	public class IdentityContext : IdentityDbContext<IdentityUser>
	{
		public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{


			base.OnModelCreating(modelBuilder);

			modelBuilder.HasDefaultSchema("Identity");

			modelBuilder.Entity<IdentityUser>(entity => { entity.ToTable(name: "Users"); });

			modelBuilder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Roles"); });

			modelBuilder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable(name: "UserRoles"); });

			modelBuilder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable(name: "UserLogin"); });







		}



	}
}

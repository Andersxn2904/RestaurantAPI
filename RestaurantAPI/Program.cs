using RestaurantAPI.Infraestructure.Persistence;
using RestaurantAPI.Core.Application;
using RestaurantAPI.Infraestructure.Identity;
using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Infraestructure.Identity.Seeds;
using Presentation.RestaurantAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerExtension();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	try
	{

		var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
		var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

		await DefaultRoles.SeedAsync(userManager, roleManager);

		await DefaultUser.SeedAsync(userManager, roleManager);
	}
	catch (Exception ex)
	{

	}

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseSwaggerExtension();
app.MapControllers();
app.UseHealthChecks("/health");


app.Run();

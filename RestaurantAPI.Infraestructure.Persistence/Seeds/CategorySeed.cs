using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAPI.Core.Application.Enums;
using RestaurantAPI.Core.Application.Helpers;
using RestaurantAPI.Core.Domain.Entities;


namespace RestaurantAPI.Infraestructure.Persistence.Seeds
{
    public class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(

            new Category { Name = GetStringValueEnum.GetStringValue(DishCategories.MAINDISH), ID = 1},
            new Category { Name = GetStringValueEnum.GetStringValue(DishCategories.DESSERT), ID = 2},
            new Category { Name = GetStringValueEnum.GetStringValue(DishCategories.BEVERAGE), ID = 3},
            new Category { Name = GetStringValueEnum.GetStringValue(DishCategories.APPETIZER), ID = 4}

            );
        }
    }
}

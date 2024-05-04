using RestaurantAPI.Core.Application.Helpers;

namespace RestaurantAPI.Core.Application.Enums
{
    public enum DishCategories
    {
        [StringValue("Main dish")]
        MAINDISH,

        [StringValue("Appetizer")]
        APPETIZER,

        [StringValue("Dessert")]
        DESSERT,

        [StringValue("Beverage")]
        BEVERAGE
    }
}

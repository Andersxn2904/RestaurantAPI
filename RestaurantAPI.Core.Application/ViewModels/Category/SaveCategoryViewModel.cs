using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Core.Application.ViewModels.Category
{
    public class SaveCategoryViewModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarStore.Models
{
    public class UpdateCar
    {
        public Guid Id { get; set; }

        [Required]
        public string Mark { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "You need to use numbers only")]
        public string Mileage { get; set; }

        public string? Color { get; set; }

        public string ImagePath { get; set; } = string.Empty;

        public string ExistingImagePath { get; set; } = string.Empty;

        [DisplayName("Upload Image")]
        public IFormFile? Image { get; set; }
    }
}


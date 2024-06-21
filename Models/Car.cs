using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CarStore.Models
{
    public class Car
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Mark is required.")]
        public string Mark {  get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "You need to use numbers only")]
        public string Mileage {  get; set; }

        public string? Color { get; set; }

        public string ImagePath { get; set; } = string.Empty;

        [Required]
        [DisplayName("Upload Image")]
        public IFormFile Image { get; set; }
    }
}

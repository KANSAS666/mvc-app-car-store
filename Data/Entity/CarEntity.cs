namespace CarStore.Models
{
    public class CarEntity
    {
        public Guid Id { get; set; }
        public string Mark { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Mileage { get; set; } = "0";
        public string Color { get; set; } = "Not";
        public string? ImagePath { get; set; }
    }
}

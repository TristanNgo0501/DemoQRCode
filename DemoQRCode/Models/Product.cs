using System.ComponentModel.DataAnnotations;

namespace DemoQRCode.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        public string? QrCodeData { get; set; }
    }
}
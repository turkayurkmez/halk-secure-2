

using System.ComponentModel.DataAnnotations;

namespace SecurityInMVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen ürün adı giriniz")]
        [MinLength(2, ErrorMessage = "Lütfen ürün adı giriniz")]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }
}

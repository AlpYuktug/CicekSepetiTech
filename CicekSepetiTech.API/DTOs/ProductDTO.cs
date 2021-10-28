using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepetiTech.API.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [RegularExpression(@"^.{3,}$", ErrorMessage = "The product name is a minimum of 3 characters.")]
        [Required(ErrorMessage = "Required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The product name is a maximum of 100 characters.")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Stock must be larger than one")]
        [Required(ErrorMessage = "Stock is required")]
        public int Stock { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Price must be larger than one")]
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
    }
}

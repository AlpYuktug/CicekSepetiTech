using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepetiTech.API.DTOs
{
    public class BasketDetailDTO
    {
        public int Id { get; set; }

        [ForeignKey("Basket")]
        [Range(1,int.MaxValue,ErrorMessage = "Basket Id must be larger than one")]
        [Required(ErrorMessage = "Basket Id is required")]
        public int BasketId { get; set; }

        [ForeignKey("Product")]
        [Range(1, int.MaxValue, ErrorMessage = "Product Id must be larger than one")]
        [Required(ErrorMessage = "Product Id is required")]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be larger than one")]
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
    }
}

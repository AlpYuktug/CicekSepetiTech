using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepetiTech.API.DTOs
{
    public class BasketDTO
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Customer Id must be larger than one")]
        [Required(ErrorMessage = "Customer Id is required")]
        public int CustomerId { get; set; }

    }
}

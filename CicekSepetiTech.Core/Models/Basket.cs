using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepetiTech.Core.Models
{
    public class Basket
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public ICollection<BasketDetail> BasketDetail { get; set; }
    }
}

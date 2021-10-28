using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepetiTech.Core.Models
{
    public class BasketDetail
    {
        [Key]
        public int Id { get; set; }
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Basket Basket{ get; set; }
        public ICollection<Product> Product { get; set; }

    }
}

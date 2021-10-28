using CicekSepetiTech.Core.Models;
using CicekSepetiTech.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepetiTech.Data.Repositories
{
    class BasketRepository : Repository<Basket>, IBasketRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public BasketRepository(AppDbContext context) : base(context)
        {
        }

    }
}

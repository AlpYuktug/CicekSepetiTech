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
    class BasketDetailRepository : Repository<BasketDetail>, IBasketDetailRepository
    {
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public BasketDetailRepository(AppDbContext context) : base(context)
        {
        }

    }
}

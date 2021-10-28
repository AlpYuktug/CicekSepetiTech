using CicekSepetiTech.Core.Repositories;
using CicekSepetiTech.Core.UnitOfWorks;
using CicekSepetiTech.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepetiTech.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private ProductRepository _productRepository;
        private BasketRepository _basketRepository;
        private BasketDetailRepository _basketDetailRepository;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IProductRepository Product => _productRepository = _productRepository ?? new ProductRepository(_context);
        public IBasketRepository Basket => _basketRepository = _basketRepository ?? new BasketRepository(_context);
        public IBasketDetailRepository BasketDetail => _basketDetailRepository = _basketDetailRepository ?? new BasketDetailRepository(_context);

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

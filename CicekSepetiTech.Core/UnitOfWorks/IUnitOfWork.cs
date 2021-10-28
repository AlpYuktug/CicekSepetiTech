using CicekSepetiTech.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepetiTech.Core.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        IBasketRepository Basket { get; }
        IBasketDetailRepository BasketDetail { get; }

        Task CommitAsync();
        void Commit();
    }
}

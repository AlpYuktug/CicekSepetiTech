using CicekSepetiTech.Core.Models;
using CicekSepetiTech.Core.Repositories;
using CicekSepetiTech.Core.Services;
using CicekSepetiTech.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepetiTech.Service.Services
{
    public class BasketService : Service<Basket>, IBasketService
    {
        public BasketService(IUnitOfWork unitOfWork, IRepository<Basket> repository) : base(unitOfWork, repository)
        {

        }
    }
}
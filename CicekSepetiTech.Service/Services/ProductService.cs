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
    public class ProductService : Service<Product>, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IRepository<Product> repository) : base(unitOfWork, repository)
        {

        }
    }
}
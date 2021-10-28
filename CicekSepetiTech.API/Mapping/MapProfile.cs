using AutoMapper;
using CicekSepetiTech.API.DTOs;
using CicekSepetiTech.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepetiTech.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<Basket, BasketDTO>();
            CreateMap<BasketDTO, Basket>();
            CreateMap<BasketDetail, BasketDetailDTO>();
            CreateMap<BasketDetailDTO, BasketDetail>();
        }
         
    }
}

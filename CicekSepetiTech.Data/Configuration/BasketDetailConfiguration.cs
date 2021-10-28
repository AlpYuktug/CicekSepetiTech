using CicekSepetiTech.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepetiTech.Data.Configuration
{
    class BasketDetailConfiguration : IEntityTypeConfiguration<BasketDetail>
    {
        public void Configure(EntityTypeBuilder<BasketDetail> builder)
        {
            builder.ToTable("BasketDetail", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.BasketId).IsRequired();
    }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(x => x.ProductBrand).WithMany(x=>x.Products).HasForeignKey(x => x.ProductBrandId);
            builder.HasOne(x => x.ProductType).WithMany(x => x.Products).HasForeignKey(x => x.ProductTypeId);
        }
    }
}

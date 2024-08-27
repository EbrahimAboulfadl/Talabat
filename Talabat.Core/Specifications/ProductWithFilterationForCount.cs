﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithFilterationForCount :BaseSpecification<Product>
    {
        public ProductWithFilterationForCount(ProductsSpecsParams Params) : base (p =>
            (!Params.BrandId.HasValue || p.ProductBrandId == Params.BrandId)
             && (!Params.TypeId.HasValue || p.ProductTypeId == Params.TypeId)
        )
        {
            
        }
    }
}

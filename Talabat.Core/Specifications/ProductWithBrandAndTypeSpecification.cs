﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(ProductsSpecsParams specsParams) :base(

            p=>(string.IsNullOrEmpty(specsParams.Search)||p.Name.Contains(specsParams.Search))
            &&
            (!specsParams.BrandId.HasValue || p.ProductBrandId == specsParams.BrandId) 
            &&  (!specsParams.TypeId.HasValue || p.ProductTypeId == specsParams.TypeId)          )
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
            if (!string.IsNullOrEmpty(specsParams.Sort)) 
            {
                switch (specsParams.Sort.ToLower()) {
                    case "priceasc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDesc(p=>p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

            ApplyPagination(specsParams.PageSize * (specsParams.PageIndex-1) , specsParams.PageSize);

        }   
        
        public ProductWithBrandAndTypeSpecification(Expression<Func<Product,bool>> criteria ) :base(criteria)

        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }  
        

    }
}

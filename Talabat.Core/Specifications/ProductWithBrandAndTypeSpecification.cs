using System;
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
        public ProductWithBrandAndTypeSpecification() :base()
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }   
        
        public ProductWithBrandAndTypeSpecification(Expression<Func<Product,bool>> criteria ) :base(criteria)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification) { 
        
             var query = inputQuery;

            if (specification is not null)
            {

                if (specification.Criteria is not null) { 
                        
                    query =  query.Where(specification.Criteria);

                }

                if (specification.OrderBy is not null)
                {

                    query = query.OrderBy(specification.OrderBy);
                }
                if (specification.OrderByDesc is not null)
                {

                    query = query.OrderByDescending(specification.OrderByDesc);
                }
                query = specification.Includes.Aggregate(query,(query, includeExpression)=>query.Include(includeExpression));
            }
            return query;
        }
    } 
}

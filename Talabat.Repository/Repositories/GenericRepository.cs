using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext dbContext;

        public GenericRepository(StoreContext  dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> GetAllAsync()=> await dbContext.Set<T>().ToListAsync();
        public async Task<T> GetByIdAsync(int id) => await dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
 
        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();

        }


        public async Task<T> GetOneWithSpecAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        private  IQueryable<T> ApplySpecification(ISpecification<T> specification) {
          return SpecificationEvaluator<T>.GetQuery(dbContext.Set<T>(), specification);
        }
    }
}

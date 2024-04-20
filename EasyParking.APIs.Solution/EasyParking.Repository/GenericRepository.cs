using EasyParking.Core.Entities;
using EasyParking.Core.Repositories;
using EasyParking.Core.Specifications;
using EasyParking.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
		private readonly StoreContext dbContext;

		public GenericRepository(StoreContext dbContext)
        {
			this.dbContext = dbContext;
		}

		#region Static Queries
		//public async Task<IEnumerable<T>> GetAllAsync()
		//{
		//	if(typeof(T) == typeof(Pakya))
		//		return (IEnumerable<T>) await dbContext.Pakyas.Include(P => P.Garage).ToListAsync();
		//	return await dbContext.Set<T>().ToListAsync();
		//}

		public async Task<T> GetByIdAsync(int id)
		{
			return await dbContext.Set<T>().FindAsync(id);
		}
		#endregion

		#region Dynamic Queries
		public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpecification(spec).ToListAsync();	
		}

      
        public async Task<T> GetByIdlWithSpecAsync(ISpecification<T> spec)
		{
			return await ApplySpecification(spec).FirstOrDefaultAsync();
		}
		#endregion

		private IQueryable<T> ApplySpecification(ISpecification<T> spec)
		{
			return SpecificationEvalutor<T>.GetQuery(dbContext.Set<T>(), spec);
		}

        public async Task Add(T Entity)
        =>await dbContext.Set<T>().AddAsync(Entity);

        public void Update(T Entity)
        => dbContext.Set<T>().Update(Entity);

        public void Delete(T Entity)
        => dbContext.Set<T>().Remove(Entity);	
    }
}

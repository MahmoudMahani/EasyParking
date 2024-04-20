using EasyParking.Core.Entities;
using EasyParking.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Core.Repositories
{
	public interface IGenericRepository<T> where T : BaseEntity
	{
		//Task<IEnumerable<T>> GetAllAsync();

		Task<T> GetByIdAsync(int id);

		Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec);

		Task<T> GetByIdlWithSpecAsync(ISpecification<T> spec);

		Task Add(T Entity);
		void Update(T Entity);
		void Delete(T Entity);

	}
}

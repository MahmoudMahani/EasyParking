using EasyParking.Core.Entities;
using EasyParking.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Repository
{
	public static class SpecificationEvalutor<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
		{
			var query = inputQuery;
			if(spec.Criteria is not  null) 
				query = query.Where(spec.Criteria);

			query = spec.Includes.Aggregate(query, (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression)); 



			return query;
		}
	}
}

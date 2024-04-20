using EasyParking.Core;
using EasyParking.Core.Entities;
using EasyParking.Core.Entities.Booking_Aggregation;
using EasyParking.Core.Repositories;
using EasyParking.Repository.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext context;
        private Hashtable repositories;
        public UnitOfWork(StoreContext context)
        {
            this.context = context;
            repositories = new Hashtable();
        }
        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var type = typeof(TEntity).Name;
            if (!repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(context);
                repositories.Add(type, repository); 
            }
            return repositories[type] as IGenericRepository<TEntity>;
        }

       
        public async Task<int> Complete()
       => await context.SaveChangesAsync();

        public async ValueTask DisposeAsync()
        => await context.DisposeAsync();

        
    }
}

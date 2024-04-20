using EasyParking.Core.Entities;
using EasyParking.Core.Entities.Booking_Aggregation;
using EasyParking.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Core
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity:BaseEntity;
        Task<int> Complete(); 
    }
}

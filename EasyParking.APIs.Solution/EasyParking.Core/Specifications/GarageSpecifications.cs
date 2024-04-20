using EasyParking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Core.Specifications
{
	public class GarageSpecifications : BaseSpecification<Garage>
	{
        public GarageSpecifications()
        {
            
        }
        public GarageSpecifications(int id) : base(G => G.Id == id)
		{ 
		}

	}
}

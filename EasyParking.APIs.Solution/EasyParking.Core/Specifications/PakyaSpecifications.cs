using EasyParking.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Core.Specifications
{
	public class PakyaSpecifications : BaseSpecification<Pakya>
	{
        public PakyaSpecifications()
        {
            Includes.Add(P => P.Garage);
        }

        public PakyaSpecifications(int id) : base(P => P.Id == id)
        {
			Includes.Add(P => P.Garage);
		}


    }
}

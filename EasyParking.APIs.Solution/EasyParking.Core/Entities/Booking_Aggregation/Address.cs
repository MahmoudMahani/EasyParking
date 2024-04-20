using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Core.Entities.Booking_Aggregation
{
    public class Address
    {
        public Address() { } 
        public Address(string firstName, string lastName, string city, string street, string numCar, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            City = city;
            Street = street;
            NumCar = numCar;
            Phone = phone;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string NumCar { get; set; }
        public string Phone { get; set; }

    }
}

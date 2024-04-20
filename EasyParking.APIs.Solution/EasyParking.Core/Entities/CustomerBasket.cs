using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyParking.Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }

        public List<BasketItem> Items { get; set; }
        public string PaymentIntentId { get; set; }
        public string ClintSecret { get; set; }

        public decimal BookingCost { get; set; }
        public CustomerBasket(string id) 
        {
            Id
                = id;
        }

    }
}

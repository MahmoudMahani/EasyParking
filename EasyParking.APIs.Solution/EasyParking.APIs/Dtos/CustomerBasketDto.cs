using EasyParking.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace EasyParking.APIs.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
      
        public List<BasketItemDto> Items { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClintSecret { get; set; }

        public decimal BookingCost { get; set; }
    }
}

namespace EasyParking.APIs.Dtos
{
    public class BookingDto
    {
        public string BasketId { get; set; }

        public AddressDto CustomerAddress { get; set; }
    }
}

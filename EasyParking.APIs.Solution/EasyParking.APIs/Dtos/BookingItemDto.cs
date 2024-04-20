namespace EasyParking.APIs.Dtos
{
    public class BookingItemDto
    {
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public bool State { get; set; }
        public int Quantity { get; set; }
    }
}

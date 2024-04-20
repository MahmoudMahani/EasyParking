namespace EasyParking.APIs.Dtos
{
	public class GarageToReturnDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string PictureUrl { get; set; }
		public string LocationUrl { get; set; }
		public bool State { get; set; }

		public decimal HourPrice { get; set; }

	}
}

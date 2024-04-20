namespace EasyParking.APIs.Dtos
{
	public class PakyaToReturnDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string PictureUrl { get; set; }
		
		public int GarageId { get; set; }

		public string Garage { get; set; }

	}
}

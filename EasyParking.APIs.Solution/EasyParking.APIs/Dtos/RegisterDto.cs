using System.ComponentModel.DataAnnotations;

namespace EasyParking.APIs.Dtos
{
	public class RegisterDto
	{
		[Required]
		public string DisplayName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public int Age { get; set; }

		[Required]
		public string CarNumber { get; set; }

		[Required]
		[Phone]
		public string PhoneNumber { get; set; }	

		public string Password { get; set; }
	}
}

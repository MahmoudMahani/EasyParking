using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyParking.APIs.Controllers
{
	
	public class TimeController : ApiBaseController
	{
		[HttpGet("currenttime")]
		public async Task<ActionResult<int>> GetTime()
		{
			int time = int.Parse(DateTime.Now.Hour.ToString());
			
			return Ok(time);
		}
	}
}

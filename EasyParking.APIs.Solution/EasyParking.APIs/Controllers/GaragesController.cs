using AutoMapper;
using EasyParking.APIs.Dtos;
using EasyParking.APIs.Errors;
using EasyParking.Core.Entities;
using EasyParking.Core.Repositories;
using EasyParking.Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyParking.APIs.Controllers
{
	public class GaragesController : ApiBaseController
	{
		private readonly IGenericRepository<Garage> garageRepo;
		private readonly IMapper mapper;

		public GaragesController(IGenericRepository<Garage> GarageRepo, IMapper mapper)
        {
			garageRepo = GarageRepo;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GarageToReturnDto>>> GetGarages()
		{
			var spec = new GarageSpecifications();
			var Garages = await garageRepo.GetAllWithSpecAsync(spec);
			var MappedGarages = mapper.Map<IEnumerable<Garage>, IEnumerable<GarageToReturnDto>>(Garages);
			return Ok(MappedGarages);
		}

		[ProducesResponseType(typeof(GarageToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public async Task<ActionResult<GarageToReturnDto>> GetGarage(int id)
		{
			var spec = new GarageSpecifications(id);
			var Garage = await garageRepo.GetByIdlWithSpecAsync(spec);
			var MappedGarage = mapper.Map<Garage,GarageToReturnDto>(Garage);
			return Ok(MappedGarage);
		}
	}
}

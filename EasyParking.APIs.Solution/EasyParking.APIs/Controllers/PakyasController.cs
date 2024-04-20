using AutoMapper;
using EasyParking.APIs.Dtos;
using EasyParking.APIs.Errors;
using EasyParking.Core.Entities;
using EasyParking.Core.Repositories;
using EasyParking.Core.Specifications;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasyParking.APIs.Controllers
{
	
	public class PakyasController : ApiBaseController
	{
		private readonly IGenericRepository<Pakya> pakyaRepo;
		private readonly IMapper mapper;

		public PakyasController(IGenericRepository<Pakya> PakyaRepo, IMapper mapper)
        {
			pakyaRepo = PakyaRepo;
			this.mapper = mapper;
		}

		
		[HttpGet]
		public async Task<ActionResult<IEnumerable<PakyaToReturnDto>>> GetPakyas()
		{
			var spec = new PakyaSpecifications();
			var Pakyas = await pakyaRepo.GetAllWithSpecAsync(spec);
			var MappedPakyas = mapper.Map<IEnumerable<Pakya>, IEnumerable<PakyaToReturnDto>>(Pakyas);
			return Ok(MappedPakyas);
		}

		[ProducesResponseType(typeof(PakyaToReturnDto),StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]

		[HttpGet("{id}")]
		public async Task<ActionResult<PakyaToReturnDto>> GetPakya(int id)
		{
			var spec = new PakyaSpecifications(id);
			var Pakya = await pakyaRepo.GetByIdlWithSpecAsync(spec);
			var MappedPakya = mapper.Map<Pakya, PakyaToReturnDto>(Pakya);
			return Ok(MappedPakya);
		}

		
	}
	
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleMongoDbDriver.Repository.Interface;
using SampleMongoDbDriver.Repository;
using static SampleMongoDbDriver.Models.Dtos;

namespace SampleMongoDbDriver.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AmphurController : ControllerBase
	{
		private readonly IDbRepository _repository;

		public AmphurController(IDbRepository repository)
		{
			_repository = repository;
		}

		[HttpGet("{provinceId}")]
		public async Task<IEnumerable<AmphurDto>> GetAsync(string provinceId)
		{
			var amphurs = (await _repository.GetAmphursAsync(provinceId)).Select(q => q.AsDto(provinceId));
			return amphurs;
		}

		[HttpGet("{provinceId}/{amphurId}")]
		public async Task<ActionResult<AmphurDto>> GetAmphurAsync(string provinceId, string amphurId)
		{
			var amphur = (await _repository.GetAmphurAsync(provinceId, amphurId));

			if (amphur == null)
			{
				return NotFound();
			}
			return Ok(amphur.AsDto(provinceId));
		}

		[HttpPost]
		public async Task PostAsync(CreateAmphurDto createAmphurDto)
		{
			await _repository.CreateAmphurAsync(createAmphurDto);
		}

		[HttpPut]
		public async Task PutAsync(string provinceId, string amphurId, UpdateAmphurDto updateAmphurDto)
		{
			await _repository.UpdateAmphurAsync(provinceId, amphurId, updateAmphurDto);
		}

		[HttpDelete]
		public async Task DeleteAsync(string provinceId, string amphurId)
		{
			await _repository.DeleteAmphurAsync(provinceId, amphurId);
		}
	}
}
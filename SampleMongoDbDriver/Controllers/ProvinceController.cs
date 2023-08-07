using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleMongoDbDriver.Repository.Interface;
using static SampleMongoDbDriver.Models.Dtos;

namespace SampleMongoDbDriver.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProvinceController : ControllerBase
	{
		private readonly IDbRepository _repository;

		public ProvinceController(IDbRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public async Task<IEnumerable<ProvinceDto>> GetAsync()
		{
			var provinces = (await _repository.GetProvincesAsync()).Select(q => q.AsDto());
			return provinces;
		}

		[HttpPost]
		public async Task PostAsync(CreateProvinceDto createProvinceDto)
		{
			await _repository.CreateProvinceAsync(createProvinceDto);
		}

		[HttpPut]
		public async Task PutAsync(string provinceId, UpdateProvinceDto updateProvinceDto)
		{
			await _repository.UpdateProvinceAsync(provinceId, updateProvinceDto);
		}

		[HttpDelete]
		public async Task DeleteAsync(string provinceId)
		{
			await _repository.DeleteProvinceAsync(provinceId);
		}
	}
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleMongoDbDriver.Models.Entities;
using SampleMongoDbDriver.Repository.Interface;
using static SampleMongoDbDriver.Models.Dtos;

namespace SampleMongoDbDriver.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DocumentController : ControllerBase
	{
		private readonly IDbRepository _repository;

		public DocumentController(IDbRepository repository)
		{
			_repository = repository;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<DocumentDto>> GetAsync(Guid id)
		{
			Document? document = await _repository.GetDocumentByDocumentIdAsync(id);

			if (document == null)
			{
				return NotFound();
			}

			return document.AsDto();
		}

		[HttpGet]
		public async Task<IEnumerable<DocumentDto>> GetAsync()
		{
			var documents = (await _repository.GetDocumentsAsync()).Select(q => q.AsDto());

			return documents;
		}

		[HttpPost]
		public async Task PostAsync(CreateDocumentDto createDocumetnDto)
		{
			await _repository.CreateDocumentAsync(createDocumetnDto);
		}

		[HttpPut]
		public async Task PutAsync(Guid documentId, UpdateDocumentDto updateDocumentDto)
		{
			await _repository.UpdateDocumentAsync(documentId, updateDocumentDto);
		}

		[HttpDelete]
		public async Task DeleteAsync(Guid documentId)
		{
			await _repository.DeleteDocumentAsync(documentId);
		}
	}
}
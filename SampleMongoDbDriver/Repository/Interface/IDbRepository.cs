using SampleMongoDbDriver.Models.Entities;
using static SampleMongoDbDriver.Models.Dtos;

namespace SampleMongoDbDriver.Repository.Interface
{
	public interface IDbRepository
	{
		#region Document

		public Task<List<Models.Entities.Document>> GetDocumentsAsync();

		public Task<Models.Entities.Document?> GetDocumentAsync(string id);

		public Task<Models.Entities.Document?> GetDocumentByDocumentIdAsync(Guid documentId);

		public Task CreateDocumentAsync(CreateDocumentDto createDocumentDto);

		public Task UpdateDocumentAsync(Guid documentId, UpdateDocumentDto updateDocumentDto);

		public Task DeleteDocumentAsync(Guid documentId);

		#endregion Document

		#region Province

		public Task<List<Province>> GetProvincesAsync();

		public Task<Province?> GetprovinceAsync(string id);

		public Task<Province?> GetProvinceByProvinceIdAsync(string provinceId);

		public Task CreateProvinceAsync(CreateProvinceDto createProvinceDto);

		public Task UpdateProvinceAsync(string provinceId, UpdateProvinceDto updateProvinceDto);

		public Task DeleteProvinceAsync(string provinceId);

		#endregion Province

		#region Amphur

		public Task<List<Amphur>> GetAmphursAsync(string provinceId);

		public Task<Amphur?> GetAmphurAsync(string provinceId, string amphurId);

		public Task CreateAmphurAsync(CreateAmphurDto createAmphurDto);

		public Task UpdateAmphurAsync(string provinceId, string amphurId, UpdateAmphurDto updateAmphurDto);

		public Task DeleteAmphurAsync(string provinceId, string amphurId);

		#endregion Amphur
	}
}
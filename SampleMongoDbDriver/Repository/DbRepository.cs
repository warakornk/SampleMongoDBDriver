using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SampleMongoDbDriver.Models;
using SampleMongoDbDriver.Models.Entities;
using SampleMongoDbDriver.Repository.Interface;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using static SampleMongoDbDriver.Models.Dtos;

namespace SampleMongoDbDriver.Repository
{
	public class DbRepository : IDbRepository
	{
		#region DbRepository

		// Prepair collections for queries
		private readonly IMongoCollection<Models.Entities.Document> _documentCollection;

		private readonly IMongoCollection<Models.Entities.Province> _provinceCollection;

		public DbRepository(IOptions<MongoDBSettings> mongoDBSettings)
		{
			var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
			var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);

			// Add collection to private value. This get from appsettings or fixed name.
			_documentCollection = mongoDatabase.GetCollection<Models.Entities.Document>(mongoDBSettings.Value.DocumentCollectionName);
			_provinceCollection = mongoDatabase.GetCollection<Models.Entities.Province>(mongoDBSettings.Value.ProvinceCollectionName);
		}

		#endregion DbRepository

		#region Document

		public async Task<List<Models.Entities.Document>> GetDocumentsAsync()
		{
			return await _documentCollection.Find(_ => true).ToListAsync();
		}

		public async Task<Models.Entities.Document?> GetDocumentAsync(string id)
		{
			// This id mean onjectId io MongoDB
			return await _documentCollection.Find(q => q.Id == id).FirstOrDefaultAsync();
		}

		public async Task<Models.Entities.Document?> GetDocumentByDocumentIdAsync(Guid documentId)
		{
			return await _documentCollection.Find(q => q.DocumentId == documentId).FirstOrDefaultAsync();
		}

		public async Task CreateDocumentAsync(CreateDocumentDto createDocumentDto)
		{
			Models.Entities.Document document = new Models.Entities.Document()
			{
				DocumentId = Guid.NewGuid(),
				DocumentName = createDocumentDto.DocumentName,
				PageCount = createDocumentDto.PageCount,
				PublishDate = createDocumentDto.PublishDate,
				SysCreateDate = DateTimeOffset.Now,
				SysCreateIP = createDocumentDto.IP,
				SysCreateUser = createDocumentDto.CreateUserName,
				SysUpdateDate = null,
				SysUpdateIP = "",
				SysUpdateUser = ""
			};

			await _documentCollection.InsertOneAsync(document);
		}

		public async Task UpdateDocumentAsync(Guid documentId, UpdateDocumentDto updateDocumentDto)
		{
			Models.Entities.Document document = await _documentCollection.Find(q => q.DocumentId == documentId).FirstOrDefaultAsync();

			if (document != null)
			{
				document.DocumentName = updateDocumentDto.DocumentName;
				document.PageCount = updateDocumentDto.PageCount;
				document.PublishDate = updateDocumentDto.PublishDate;
				document.SysUpdateDate = DateTimeOffset.Now;
				document.SysUpdateIP = updateDocumentDto.IP;
				document.SysUpdateUser = updateDocumentDto.UpdateUserName;

				await _documentCollection.ReplaceOneAsync(q => q.Id == document.Id, document);
			}
		}

		public async Task DeleteDocumentAsync(Guid documentId)
		{
			Models.Entities.Document document = await _documentCollection.Find(q => q.DocumentId == documentId).FirstOrDefaultAsync();

			if (document != null)
			{
				await _documentCollection.DeleteOneAsync(q => q.Id == document.Id);
			}
		}

		#endregion Document

		#region Province

		public async Task<List<Province>> GetProvincesAsync()
		{
			return await _provinceCollection.Find(_ => true).ToListAsync();
		}

		public async Task<Province?> GetprovinceAsync(string id)
		{
			return await _provinceCollection.Find(q => q.Id == id).FirstOrDefaultAsync();
		}

		public async Task<Province?> GetProvinceByProvinceIdAsync(string provinceId)
		{
			return await _provinceCollection.Find(q => q.ProvinceId == provinceId).FirstOrDefaultAsync();
		}

		public async Task CreateProvinceAsync(CreateProvinceDto createProvinceDto)
		{
			Province province = new Province()
			{
				ProvinceId = createProvinceDto.ProvinceId,
				ProvinceName = createProvinceDto.provinceName,
				Amphurs = createProvinceDto.Amphurs ?? new List<Amphur>()
			};

			await _provinceCollection.InsertOneAsync(province);
		}

		public async Task UpdateProvinceAsync(string provinceId, UpdateProvinceDto updateProvinceDto)
		{
			Province province = await _provinceCollection.Find(q => q.ProvinceId == provinceId).FirstOrDefaultAsync();

			if (province != null)
			{
				province.ProvinceId = updateProvinceDto.ProvinceId;
				province.ProvinceName = updateProvinceDto.provinceName;
				province.Amphurs = updateProvinceDto.Amphurs ?? new List<Amphur>();

				await _provinceCollection.ReplaceOneAsync(q => q.Id == province.Id, province);
			}
		}

		public async Task DeleteProvinceAsync(string provinceId)
		{
			Province province = await _provinceCollection.Find(q => q.ProvinceId == provinceId).FirstOrDefaultAsync();

			if (province != null)
			{
				await _provinceCollection.DeleteOneAsync(q => q.Id == province.Id);
			}
		}

		#endregion Province

		#region Amphur

		public async Task<List<Amphur>> GetAmphursAsync(string provinceId)
		{
			Province province = await _provinceCollection.Find(q => q.ProvinceId == provinceId).SingleOrDefaultAsync();

			return province.Amphurs.ToList();
		}

		public async Task<Amphur?> GetAmphurAsync(string provinceId, string amphurId)
		{
			Province province = await _provinceCollection.Find(q => q.ProvinceId == provinceId).SingleOrDefaultAsync();

			return province.Amphurs.FirstOrDefault(q => q.AmphurId == amphurId);
		}

		public async Task CreateAmphurAsync(CreateAmphurDto createAmphurDto)
		{
			Province province = await _provinceCollection.Find(q => q.ProvinceId == createAmphurDto.provinceId).SingleOrDefaultAsync();

			if (province != null)
			{
				Amphur amphur = new Amphur()
				{
					AmphurId = createAmphurDto.AmphurId,
					AmphurName = createAmphurDto.AmphurName
				};
				province.Amphurs.Add(amphur);
				UpdateProvinceDto updateProvinceDto = new UpdateProvinceDto(province.ProvinceId, province.ProvinceName, province.Amphurs);

				await UpdateProvinceAsync(createAmphurDto.provinceId, updateProvinceDto);
			}
		}

		public async Task UpdateAmphurAsync(string provinceId, string amphurId, UpdateAmphurDto updateAmphurDto)
		{
			Province province = await _provinceCollection.Find(q => q.ProvinceId == provinceId).SingleOrDefaultAsync();

			if (province != null)
			{
				foreach (Amphur amphur in province.Amphurs)
				{
					if (amphur.AmphurId == amphurId)
					{
						amphur.AmphurId = updateAmphurDto.AmphurId;
						amphur.AmphurName = updateAmphurDto.AmphurName;
					}
				}

				UpdateProvinceDto updateProvinceDto = new UpdateProvinceDto(province.ProvinceId, province.ProvinceName, province.Amphurs);

				await UpdateProvinceAsync(provinceId, updateProvinceDto);
			}
		}

		public async Task DeleteAmphurAsync(string provinceId, string amphurId)
		{
			// Delete amphur can't delete from collection so make new collection and add old data except delete data.
			Province province = await _provinceCollection.Find(q => q.ProvinceId == provinceId).SingleOrDefaultAsync();

			if (province != null)
			{
				// Create new amphur list without deleted amphur then add back to updateProvince
				List<Amphur> acceptAmphurs = province.Amphurs.Where(q => q.AmphurId != amphurId).ToList();
				UpdateProvinceDto updateProvinceDto = new UpdateProvinceDto(province.ProvinceId, province.ProvinceName, acceptAmphurs);

				await UpdateProvinceAsync(provinceId, updateProvinceDto);
			}
		}

		#endregion Amphur
	}
}
using SampleMongoDbDriver.Models.Entities;

namespace SampleMongoDbDriver.Models
{
	public class Dtos
	{   // Create DTO for exchange data between frontend and backend to database
		// Use record for normalItemm, createItem and updateItem. each properties like item's model but cut some propereties off or add some properties for exchange data
		// When use model to DTO. This project use extension as AsDto() which define in extension.cs
		// Coution: Subdocument in collection can be null. Ex, Amphur in province should use nullable variable

		#region Document

		public record DocumentDto(object Id, Guid DocumentId, string DocumentName, int PageCount, DateTimeOffset? PublishDate);
		public record CreateDocumentDto(string DocumentName, int PageCount, DateTimeOffset? PublishDate, string IP, string CreateUserName);
		public record UpdateDocumentDto(string DocumentName, int PageCount, DateTimeOffset? PublishDate, string IP, string UpdateUserName);

		#endregion Document

		#region Province

		public record ProvinceDto(object Id, string ProvinceId, string provinceName, List<Amphur>? Amphurs);
		public record CreateProvinceDto(string ProvinceId, string provinceName, List<Amphur>? Amphurs);
		public record UpdateProvinceDto(string ProvinceId, string provinceName, List<Amphur>? Amphurs);

		#endregion Province

		#region Amphur

		public record AmphurDto(string provinceId, string AmphurId, string Amphurname);
		public record CreateAmphurDto(string provinceId, string AmphurId, string AmphurName);
		public record UpdateAmphurDto(string provinceId, string AmphurId, string AmphurName);

		#endregion Amphur
	}
}
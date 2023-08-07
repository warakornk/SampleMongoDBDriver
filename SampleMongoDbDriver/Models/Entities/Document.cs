using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SampleMongoDbDriver.Models.Entities
{
	public class Document
	{
		[BsonId][BsonRepresentation(BsonType.ObjectId)] public string? Id { get; set; }
		public Guid DocumentId { get; set; }
		public string DocumentName { get; set; } = string.Empty;
		public int PageCount { get; set; } = 0;
		public DateTimeOffset? PublishDate { get; set; } = null;
		public string SysCreateUser { get; set; } = "";
		public string SysCreateIP { get; set; } = "";
		public DateTimeOffset SysCreateDate { get; set; } = DateTimeOffset.Now;
		public string? SysUpdateUser { get; set; } = "";
		public string? SysUpdateIP { get; set; } = "";
		public DateTimeOffset? SysUpdateDate { get; set; } = null;
	}
}
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SampleMongoDbDriver.Models.Entities
{
	public class Province
	{
		[BsonId][BsonRepresentation(BsonType.ObjectId)] public string? Id { get; set; }
		public string ProvinceId { get; set; } = "";
		public string ProvinceName { get; set; } = "";
		public List<Amphur> Amphurs { get; set; } = new List<Amphur>();
	}
}
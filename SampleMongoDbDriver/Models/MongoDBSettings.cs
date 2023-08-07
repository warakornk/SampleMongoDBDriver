namespace SampleMongoDbDriver.Models
{
	public class MongoDBSettings
	{
		public string ConnectionString { get; set; } = null!;
		public string DatabaseName { get; set; } = null!;
		public string DocumentCollectionName { get; set; } = null!;
		public string ProvinceCollectionName { get; set; } = null!;
	}
}
using SampleMongoDbDriver.Models.Entities;
using static SampleMongoDbDriver.Models.Dtos;

namespace SampleMongoDbDriver
{
	// This class use fro convert model to itemDto.
	// Each model will define AsDto function here.
	// This class and all functions must use static for easy to call at the end of data.

	public static class Extension
	{
		public static DocumentDto AsDto(this Document document)
		{
			return new DocumentDto(document.Id,
				document.DocumentId,
				document.DocumentName,
				document.PageCount,
				document.PublishDate
				);
		}

		public static ProvinceDto AsDto(this Province province)
		{
			return new ProvinceDto(province.Id,
				province.ProvinceId,
				province.ProvinceName,
				province.Amphurs
				);
		}

		public static AmphurDto AsDto(this Amphur amphur, string provinceId)
		{
			return new AmphurDto(
					provinceId,
					amphur.AmphurId,
					amphur.AmphurName
				);
		}
	}
}
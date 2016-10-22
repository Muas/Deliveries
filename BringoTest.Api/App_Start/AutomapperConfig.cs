using AutoMapper;
using BringoTest.Api.Mappings;

namespace BringoTest.Api
{
	public class AutomapperConfig
	{
		public static MapperConfiguration Register()
		{
			var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile<DeliveryMappingProfile>(); });
			mapperConfig.AssertConfigurationIsValid();
			return mapperConfig;
		}
	}
}
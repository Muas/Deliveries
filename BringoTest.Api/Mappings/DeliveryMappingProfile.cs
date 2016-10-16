using AutoMapper;
using BringoTest.Api.Models;

namespace BringoTest.Api.Mappings
{
	public class DeliveryMappingProfile : Profile
	{
		public DeliveryMappingProfile()
		{
			CreateMap<Delivery, Data.Models.Delivery>().ReverseMap();
		}
	}
}
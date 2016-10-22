using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BringoTest.Api.Models;
using BringoTest.Data.Repositories;
using BringoTest.Shared;
using BringoTest.Shared.Enums;
using BringoTest.Shared.Exceptions;
using BringoTest.Shared.Extensions;

namespace BringoTest.Api.Controllers
{
	[RoutePrefix("api/deliveries")]
	public class DeliveriesController : ApiController
	{
		private readonly IRepository<Data.Models.Delivery> _repository;
		private readonly IMapper _mapper;

		public DeliveriesController(IRepository<Data.Models.Delivery> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		[Route("GetAvailableDeliveries")]
		[HttpGet]
		public IEnumerable<Delivery> GetAvailableDeliveries()
		{
			return _repository
				.GetAll(x => x.Status == (int)DeliveryStatus.Available)
				.MapAll<Delivery>(_mapper);
		}

		[Route("{deliveryId}/TakeDelivery")]
		[HttpPut]
		public Delivery TakeDelivery(int deliveryId, uint userId)
		{
			var delivery = _repository.Get(deliveryId).Map<Delivery>(_mapper);
			if (delivery.Status != DeliveryStatus.Available)
			{
				throw new BadRequestException();
			}
			delivery.UserId = userId;
			delivery.Status = DeliveryStatus.Taken;
			_repository.Update(delivery.Map<Data.Models.Delivery>(_mapper));
			return delivery;
		}
	}
}
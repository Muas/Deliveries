using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using BringoTest.Api.Models;
using BringoTest.Data.Repositories;
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

		[Route("TakeDelivery")]
		[HttpPut]
		public Delivery TakeDelivery(uint deliveryId, uint userId)
		{
			throw new NotImplementedException();
//			return _repository.TakeDelivery(deliveryId, userId).Map<Delivery>(_mapper);
		}
	}
}
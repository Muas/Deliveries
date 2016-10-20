using BringoTest.Data.Models;
using BringoTest.Data.Repositories;
using BringoTest.Shared;
using BringoTest.Shared.DataTimeProvider;
using BringoTest.Shared.Extensions;

namespace BringoTest.Tasks.Tasks
{
	public sealed class ExpireDeliveriesTaskContext
	{
		
	}

	internal sealed class ExpireDeliveriesTask : ITask<ExpireDeliveriesTaskContext>
	{
		private readonly IRepository<Delivery> _repository;
		private readonly IDateTimeProvider _dateTimeProvider;

		public ExpireDeliveriesTask(IRepository<Delivery> repository, IDateTimeProvider dateTimeProvider)
		{
			_repository = repository;
			_dateTimeProvider = dateTimeProvider;
		}

		public void Execute(ExpireDeliveriesTaskContext context)
		{
			var now = _dateTimeProvider.Now();
			var expiredDeliveries = _repository.GetAll(x => x.Status == (int) DeliveryStatus.Available && x.ExpirationTime <= now)
				.ForEach(x => x.Status = (int) DeliveryStatus.Expired);
			_repository.Update(expiredDeliveries);
		}
	}
}
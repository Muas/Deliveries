using System;
using BringoTest.Data.Models;
using BringoTest.Data.Repositories;
using BringoTest.Shared.DataTimeProvider;

namespace BringoTest.Tasks.Tasks
{
	public sealed class CreateDeliveriesTaskContext
	{
		public CreateDeliveriesTaskContext(TimeSpan expirationOffset)
		{
			ExpirationOffset = expirationOffset;
		}

		public TimeSpan ExpirationOffset { get; }
	}

	internal sealed class CreateDeliveriesTask: ITask<CreateDeliveriesTaskContext>
	{
		private readonly IRepository<Delivery> _repository;
		private readonly IDateTimeProvider _dateTimeProvider;

		public CreateDeliveriesTask(IRepository<Delivery> repository, IDateTimeProvider dateTimeProvider)
		{
			_repository = repository;
			_dateTimeProvider = dateTimeProvider;
		}

		public void Execute(CreateDeliveriesTaskContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			var newDelivery = new Delivery
			{
				Status = 1,
				ExpirationTime =  _dateTimeProvider.Now().Add(context.ExpirationOffset)
			};
			_repository.Create(newDelivery);
		}
	}
}
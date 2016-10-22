using System;
using BringoTest.Data.Models;
using BringoTest.Data.Repositories;
using BringoTest.Shared;
using BringoTest.Shared.DataTimeProvider;
using BringoTest.Shared.Enums;
using BringoTest.Shared.RandomGenerator;

namespace BringoTest.Tasks.Tasks
{
	internal sealed class CreateDeliveriesTask: ITask<CreateDeliveriesTaskContext>
	{
		private readonly IRepository<Delivery> _repository;
		private readonly IDateTimeProvider _dateTimeProvider;
		private readonly IRandomGenerator _randomGenerator;

		public CreateDeliveriesTask(IRepository<Delivery> repository, IDateTimeProvider dateTimeProvider, IRandomGenerator randomGenerator)
		{
			_repository = repository;
			_dateTimeProvider = dateTimeProvider;
			_randomGenerator = randomGenerator;
		}

		public void Execute(CreateDeliveriesTaskContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			var taskCount = _randomGenerator.NextInt(context.MinTaskCount, context.MaxTaskCount);

			for (var i = 0; i < taskCount; i++)
			{
				var newDelivery = new Delivery
				{
					Status = (int) DeliveryStatus.Available,
					Title = _randomGenerator.NextString(10),
					ExpirationTime = _dateTimeProvider.Now().Add(context.ExpirationOffset)
				};
				_repository.Create(newDelivery);
			}
		}
	}
}
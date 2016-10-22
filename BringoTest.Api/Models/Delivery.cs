using System;
using BringoTest.Shared;

namespace BringoTest.Api.Models
{
	public class Delivery
	{
		public int Id { get; set; }
		public DeliveryStatus Status { get; set; }
		public string Title { get; set; }
		public uint? UserId { get; set; }
		public DateTime CreationTime { get; set; }
		public DateTime? ModificationTime { get; set; }
		public DateTime ExpirationTime { get; set; }
	}
}
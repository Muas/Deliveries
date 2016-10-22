using System;
using System.Runtime.Serialization;
using BringoTest.Shared;
using BringoTest.Shared.Enums;

namespace BringoTest.Api.Models
{
	[DataContract]
	public class Delivery
	{
		[DataMember]
		public int Id { get; set; }

		[DataMember]
		public DeliveryStatus Status { get; set; }

		[DataMember]
		public string Title { get; set; }

		[DataMember]
		public uint? UserId { get; set; }

		[DataMember]
		public DateTime CreationTime { get; set; }

		[DataMember]
		public DateTime? ModificationTime { get; set; }

		[IgnoreDataMember]
		public DateTime ExpirationTime { get; set; }
	}
}
using System;
using SQLite;

namespace BringoTest.Data.Models
{
	public class Delivery : IEntity
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public int Status { get; set; }
		public string Title { get; set; }

		public uint? UserId { get; set; }
		public DateTime CreationTime { get; set; }
		public DateTime ModificationTime { get; set; }

		public DateTime ExpirationTime { get; set; }
	}
}
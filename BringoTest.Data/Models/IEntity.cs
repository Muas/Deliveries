using System;

namespace BringoTest.Data.Models
{
	public interface IEntity
	{
		int Id { get; set; }
		DateTime CreationTime { get; set; }
		DateTime ModificationTime { get; set; }
	}
}
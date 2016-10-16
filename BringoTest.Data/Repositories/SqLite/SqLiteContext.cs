using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using BringoTest.Data.Models;

namespace BringoTest.Data.Repositories.SqLite
{
	internal class SqLiteContext : DbContext
	{
		public DbSet<Delivery> Deliveries { get; set; }
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Delivery>()
				.HasKey(x => x.Id);
			modelBuilder.Entity<Delivery>()
				.Property(x => x.Id)
				.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
		}
	}
}
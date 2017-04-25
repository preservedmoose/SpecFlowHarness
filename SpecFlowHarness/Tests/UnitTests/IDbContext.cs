using System;
using System.Data.Entity;

namespace PreservedMoose.SpecFlowHarness.UnitTests
{
	public interface IDbContext : IDisposable
	{
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
	}
}

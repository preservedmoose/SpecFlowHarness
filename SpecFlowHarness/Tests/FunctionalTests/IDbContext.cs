using System;
using System.Data.Entity;

namespace PreservedMoose.SpecFlowHarness.FunctionalTests
{
	public interface IDbContext : IDisposable
	{
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
	}
}

using System;
using System.Data.Entity;

namespace CIAndT.SpecFlowHarness.FunctionalTests
{
	public interface IDbContext : IDisposable
	{
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
	}
}

using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TinyIoC;

namespace PreservedMoose.SpecFlowHarness.FunctionalTests.Fixtures
{
	public class BaseFixture
	{
		protected enum HttpVerb
		{
			None = 0,
			Get,
			Post,
			Put,
			Delete
		}

		public TinyIoCContainer BaseContainer { get; }

		protected BaseFixture()
		{
			BaseContainer = new TinyIoCContainer();
		}

		// ---------------------------------------------------------------------------------------------

		/// <summary>
		/// This method runs only one time in the begin, before the FIRST Test
		/// Override this to setup common things that will be used by the whole Fixture
		/// </summary>
		[OneTimeSetUp]
		public virtual void OneTimeSetUp()
		{
		}

		/// <summary>
		/// This method runs before EACH test
		/// Override this method to setup/renew things that will be used/done for the Test that will start
		/// </summary>
		[SetUp]
		public virtual void AllTestsSetup()
		{
		}

		/// <summary>
		/// This method runs only one time in the end, after the LAST Test
		/// Override this to wrap up the Fixture file and clear anything that might have been
		/// left behind for the Fixture
		/// </summary>
		[OneTimeTearDown]
		public virtual void OneTimeTearDown()
		{
		}

		/// <summary>
		/// This method will run after EACH test
		/// Override this to clear wrap up the Test and clear anything that might have been
		/// left behind by the Test that just finished
		/// </summary>
		[TearDown]
		public virtual void AllTestsTearDown()
		{
		}

		// ---------------------------------------------------------------------------------------------

		/// <summary>
		/// Get a copy of the container (use for each test).
		/// </summary>
		/// <returns></returns>
		protected TinyIoCContainer GetTestContainer()
		{
			var testContainer = BaseContainer.GetChildContainer();
			return testContainer;
		}

		// ---------------------------------------------------------------------------------------------

		/// <summary>
		/// Consistent method name for testing that the container returns the correct instance for the interface.
		/// </summary>
		// ReSharper disable once InconsistentNaming
		public virtual void Container_should_return_instance_from_interface()
		{
		}

		/// <summary>
		/// A helper method to test that the interface is configured in the container.
		/// </summary>
		/// <typeparam name="TI">The type of the Interface.</typeparam>
		/// <typeparam name="TC">The type of the Concrete class.</typeparam>
		/// <param name="container">The container.</param>
		protected void TestInterface<TI, TC>(TinyIoCContainer container)
			where TI : class
			where TC : class
		{
			// Act
			var instance = container.Resolve<TI>();

			// Assert
			instance.Should().BeOfType<TC>();
		}

		/// <summary>
		/// A helper method to test that the interface is configured in the container.
		/// </summary>
		/// <typeparam name="TI">The type of the Interface.</typeparam>
		/// <typeparam name="TC">The type of the Concrete class.</typeparam>
		protected void TestInterface<TI, TC>()
			where TI : class
			where TC : class
		{
			var container = GetTestContainer();

			TestInterface<TI, TC>(container);
		}

		/// <summary>
		/// A helper method to test that the repository interface is configured in the container.
		/// </summary>
		/// <typeparam name="TI">The type of the Interface.</typeparam>
		/// <typeparam name="TC">The type of the Concrete class.</typeparam>
		protected void TestRepositoryInterface<TI, TC>()
			where TI : class
			where TC : class
		{
			var container = GetTestContainer();

			container.InstantiateAndInjectMock<IDbContext>();

			TestInterface<TI, TC>(container);
		}

		// ---------------------------------------------------------------------------------------------

		/// <summary>
		/// Mocks a context and sets a list of objects to be returned by the repository
		/// </summary>
		/// <typeparam name="TContext">The context of the module</typeparam>
		/// <typeparam name="TModel">The model of the repository</typeparam>
		/// <param name="container">The container</param>
		/// <param name="mockedCollection">The collection that should be returned from the mocked repository</param>
		protected void MockContext<TContext, TModel>(TinyIoCContainer container, ICollection<TModel> mockedCollection)
			where TContext : class, IDbContext
			where TModel : class
		{
			var mockedContext = container.InstantiateAndInjectMock<TContext>();

			var queryable = mockedCollection.AsQueryable();

			var dbSet = new Mock<DbSet<TModel>>();
			dbSet.As<IQueryable<TModel>>().Setup(m => m.Provider).Returns(queryable.Provider);
			dbSet.As<IQueryable<TModel>>().Setup(m => m.Expression).Returns(queryable.Expression);
			dbSet.As<IQueryable<TModel>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
			dbSet.As<IQueryable<TModel>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator);
			dbSet.Setup(s => s.Add(It.IsAny<TModel>())).Callback<TModel>(mockedCollection.Add);
			dbSet.Setup(s => s.Include(It.IsAny<string>())).Returns(dbSet.Object);
			dbSet.Setup(x => x.AsNoTracking()).Returns(dbSet.Object);

			mockedContext.Setup(x => x.Set<TModel>()).Returns(dbSet.Object);
		}

		/// <summary>
		/// A helper method to get the value for the specified property in the anonymous type.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="jsonResult">The json result.</param>
		/// <param name="property">The property.</param>
		/// <returns></returns>
		protected T GetPropertyValueFromAnonymousType<T>(JsonResult jsonResult, string property) where T : class
		{
			var propertyValue = jsonResult.Data
				.GetType()
				.GetProperty(property, BindingFlags.Instance | BindingFlags.Public)
				.GetValue(jsonResult.Data, null);

			var typedPropertyValue = propertyValue as T;
			return typedPropertyValue;
		}

		/// <summary>
		/// Returns the method that fits the signature.
		/// </summary>
		/// <typeparam name="T">The type that has the method we want.</typeparam>
		/// <param name="actionName">The name of the action (method).</param>
		/// <param name="httpVerb">The HTTP verb.</param>
		/// <returns>The MethodInfo descriptor.</returns>
		protected MethodInfo GetMethod<T>(string actionName, HttpVerb httpVerb = HttpVerb.None)
		{
			MethodInfo methodInfo;

			var methods = typeof(T).GetMethods().Where(m => m.Name == actionName).ToList();

			switch (httpVerb)
			{
				case HttpVerb.Put:
					{
						methodInfo = methods.Single(m => m.CustomAttributes.Any(ca => ca.AttributeType == typeof(HttpPutAttribute)));
						break;
					}
				case HttpVerb.Post:
					{
						methodInfo = methods.Single(m => m.CustomAttributes.Any(ca => ca.AttributeType == typeof(HttpPostAttribute)));
						break;
					}
				case HttpVerb.Delete:
					{
						methodInfo = methods.Single(m => m.CustomAttributes.Any(ca => ca.AttributeType == typeof(HttpDeleteAttribute)));
						break;
					}
				case HttpVerb.Get:
					{
						methodInfo = methods.Single(m => m.CustomAttributes.Any(ca => ca.AttributeType == typeof(HttpGetAttribute)));
						break;
					}
				default:
					{
						methodInfo = methods.Single();
						break;
					}
			}
			return methodInfo;
		}
	}
}

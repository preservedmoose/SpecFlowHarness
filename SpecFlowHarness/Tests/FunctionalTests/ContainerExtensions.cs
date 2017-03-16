using Moq;
using TinyIoC;

namespace CIAndT.SpecFlowHarness.FunctionalTests
{
	/// <summary>
	/// Contains extension methods For TinyIoCContainer
	/// </summary>
	public static class ContainerExtensions
	{
		/// <summary>
		/// Ejects all instances and then injects the specified mock into container.
		/// </summary>
		/// <typeparam name="T">Class to inject</typeparam>
		/// <param name="container">The container.</param>
		/// <param name="injectedType">The injected type.</param>
		public static void EjectAndInject<T>(this TinyIoCContainer container, T injectedType) where T : class
		{
			//container.EjectAllInstancesOf<T>();
			container.Register(typeof(T), injectedType);
		}

		/// <summary>
		/// Ejects all instances and then injects the specified mock into container.
		/// </summary>
		/// <typeparam name="T">Class to inject</typeparam>
		/// <param name="container">The container.</param>
		/// <param name="mock">The mock.</param>
		public static void EjectAndInjectMock<T>(this TinyIoCContainer container, Mock<T> mock) where T : class
		{
			//container.EjectAllInstancesOf<T>();
			container.Register(typeof(T), mock.Object);
		}

		/// <summary>
		/// Initializes an instance of the mock with default behavior, ejects all instances and then inject
		/// </summary>
		/// <returns>Mocked instance of T</returns>
		public static Mock<T> InstantiateAndInjectMock<T>(this TinyIoCContainer container, bool callBase = false) where T : class
		{
			var mock = new Mock<T> { CallBase = callBase };

			container.EjectAndInjectMock(mock);

			return mock;
		}

		/// <summary>
		/// Initializes an instance of the mock with default behavior and with the given constructor arguments for the class (Only valid when T is a class), ejects all instances and then inject
		/// </summary>
		/// <returns>Mocked instance of T</returns>
		/// <remarks>
		/// The mock will try to find the best match constructor given the constructor arguments, and invoke that to initialize the instance. This applies only for classes, not interfaces.
		/// </remarks>
		/// <param name="container">The container.</param>
		/// <param name="args">Optional constructor arguments if the mocked type is a class.</param>
		public static Mock<T> InstantiateAndInjectMock<T>(this TinyIoCContainer container, params object[] args) where T : class
		{
			var mock = new Mock<T>(args);

			container.EjectAndInjectMock(mock);

			return mock;
		}
	}
}

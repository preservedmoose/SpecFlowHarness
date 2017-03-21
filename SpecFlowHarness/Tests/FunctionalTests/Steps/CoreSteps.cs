using TinyIoC;

namespace PreservedMoose.SpecFlowHarness.FunctionalTests.Steps
{
	public class CoreSteps : BaseSteps
	{
		protected static readonly TinyIoCContainer Container;

		protected CalculatorData Data { get; }

		static CoreSteps()
		{
			Container = new TinyIoCContainer();
		}

		public CoreSteps(CalculatorData calculatorData)
		{
			Data = calculatorData;
		}
	}
}

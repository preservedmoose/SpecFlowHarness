using TechTalk.SpecFlow;

namespace PreservedMoose.SpecFlowHarness.AcceptanceTests
{
	[Binding]
	public class Hooks : BaseSteps
	{
		public static bool CannotExecuteBeforeFeature { get; set; }

		private static volatile bool _initialised;

		// ---------------------------------------------------------------------------------------

		[BeforeTestRun]
		public static void BeforeTestRun()
		{
		}

		[AfterTestRun]
		public static void AfterTestRun()
		{
		}

		// ---------------------------------------------------------------------------------------

		[BeforeFeature]
		public static void BeforeFeature()
		{
			if (CannotExecuteBeforeFeature) return;

			if (!_initialised)
			{
				// perform initialisation
				// ...

				_initialised = true;
			}
		}

		[AfterFeature]
		public static void AfterFeature()
		{
		}

		// ---------------------------------------------------------------------------------------

		[BeforeScenario]
		public static void BeforeScenario()
		{
		}

		[AfterScenario]
		public virtual void AfterScenario()
		{
		}

		[BeforeScenario("specFlowTag")]
		public static void BeforeScenario_Tag()
		{
		}

		[AfterScenario("specFlowTag")]
		public static void AfterScenario_Tag()
		{
		}

		// ---------------------------------------------------------------------------------------

		[BeforeStep]
		public static void BeforeStep()
		{
		}

		[AfterStep]
		public static void AfterStep()
		{
		}

		// ---------------------------------------------------------------------------------------
	}
}

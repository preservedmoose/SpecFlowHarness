using System.Collections.Generic;
using PreservedMoose.SpecFlowHarness.FunctionalTests.StepRows;
using TechTalk.SpecFlow;

namespace PreservedMoose.SpecFlowHarness.FunctionalTests.Steps
{
	[Binding]
	[Scope(Tag = "Core")]
	public class CoreGivenSteps : CoreSteps
	{
		//--------------------------------------------------------------------------

		public CoreGivenSteps(CalculatorData calculatorData)
			: base(calculatorData)
		{
		}

		//--------------------------------------------------------------------------
		// ReSharper disable UnusedMember.Global

		[Given(@"we test case sensitivity")]
		public void Given_WeTestCaseSensitivity_Lower()
		{
			SetCurrentCulture();
		}

		[Given(@"we test Case Sensitivity")]
		public void Given_WeTestCaseSensitivity_Pascal()
		{
			SetCurrentCulture();
		}

		[Given(@"we have these values:")]
		public void Given_WeHaveTheseValues(IReadOnlyCollection<StepCalculatorRow> rows)
		{
			SetCurrentCulture();

			Data.Rows = rows;
		}

		// ReSharper restore UnusedMember.Global
		//--------------------------------------------------------------------------
	}
}

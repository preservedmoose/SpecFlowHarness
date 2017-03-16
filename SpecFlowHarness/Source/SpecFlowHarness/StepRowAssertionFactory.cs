namespace CIAndT.SpecFlowHarness
{
	internal static class StepRowAssertionFactory
	{
		public static StepRowAssertion<T> Should<T>(this T actualValue)
		{
			var stepRowAssertion = new StepRowAssertion<T>(actualValue);
			return stepRowAssertion;
		}
	}
}

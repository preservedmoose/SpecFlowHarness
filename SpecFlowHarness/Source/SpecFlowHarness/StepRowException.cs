using System;

namespace PreservedMoose.SpecFlowHarness
{
	public class StepRowException : ApplicationException
	{
		public StepRowException()
		{
		}

		public StepRowException(string message)
			: base(message)
		{
		}

		public StepRowException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}

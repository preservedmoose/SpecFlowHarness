﻿using System.Globalization;
using System.Threading;

using TechTalk.SpecFlow;

namespace PreservedMoose.SpecFlowHarness
{
	public class BaseSteps : Steps
	{
		protected void SetCurrentCulture(CultureInfo currentCulture = null)
		{
			Thread.CurrentThread.CurrentCulture = currentCulture ?? new CultureInfo("en-GB");
		}
	}
}

using System.Collections.Generic;

namespace CIAndT.SpecFlowHarness.FunctionalTests.TestClasses
{
	public class YearMonthComparer : Comparer<YearMonth>
	{
		public static YearMonthComparer Instance { get; } = new YearMonthComparer();

		private YearMonthComparer()
		{
		}

		public override int Compare(YearMonth x, YearMonth y)
		{
			var compareTo = x.Value.CompareTo(y.Value);
			return compareTo;
		}
	}
}

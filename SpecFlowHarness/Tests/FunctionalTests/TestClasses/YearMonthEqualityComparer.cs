using System.Collections.Generic;

namespace PreservedMoose.SpecFlowHarness.FunctionalTests.TestClasses
{
	public class YearMonthEqualityComparer : EqualityComparer<YearMonth>
	{
		public static YearMonthEqualityComparer Instance { get; } = new YearMonthEqualityComparer();

		private YearMonthEqualityComparer()
		{
		}

		public override bool Equals(YearMonth x, YearMonth y)
		{
			var isEqual = x.Value.Equals(y.Value);
			return isEqual;
		}

		public override int GetHashCode(YearMonth obj)
		{
			var hash = obj.GetHashCode();
			return hash;
		}
	}
}

namespace PreservedMoose.SpecFlowHarness
{
	public static class Constants
	{
		public const string Space = " ";
		public const string CommaSeparator = ", ";

		public const int HashPrime = 397;

		public const string IsoYearMonth = "yyyy-MM";

		public const string IsoDate = "yyyy-MM-dd";

		public const string IsoDateTime = "yyyy-MM-dd HH:mm:ss";
		public const string IsoDateTimePrecisionMinutes = "yyyy-MM-dd HH:mm";

		public static readonly string[] IsoDateTimePrecisionMilliseconds =
		{
			"yyyy-MM-dd HH:mm:ss.f",
			"yyyy-MM-dd HH:mm:ss.ff",
			"yyyy-MM-dd HH:mm:ss.fff",
			"yyyy-MM-dd HH:mm:ss.ffff",
			"yyyy-MM-dd HH:mm:ss.fffff",
			"yyyy-MM-dd HH:mm:ss.ffffff",
			"yyyy-MM-dd HH:mm:ss.fffffff"
		};
	}
}

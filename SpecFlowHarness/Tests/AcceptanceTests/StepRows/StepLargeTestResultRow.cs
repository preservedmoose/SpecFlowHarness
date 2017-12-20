using System;
using System.Collections.Generic;
using System.Linq;

using TechTalk.SpecFlow;

namespace PreservedMoose.SpecFlowHarness.AcceptanceTests.StepRows
{
	// ReSharper disable BuiltInTypeReferenceStyle
	// ReSharper disable UnusedAutoPropertyAccessor.Local
	[Binding]
	public sealed class StepLargeTestResultRow : StepRow<StepLargeTestResultRow>, IEquatable<StepLargeTestResultRow>
	{
		public Int32 Quantity1 { get; private set; }
		public Int32 Quantity2 { get; private set; }
		public Int32 Quantity3 { get; private set; }
		public Int32 Quantity4 { get; private set; }
		public Int32 Quantity5 { get; private set; }
		public Int32 Quantity6 { get; private set; }
		public DateTime PurchaseDate { get; private set; }

		public bool Equals(StepLargeTestResultRow other)
		{
			var isEqual = HelperEquals(other);
			return isEqual;
		}

		public static IReadOnlyCollection<StepLargeTestResultRow> Convert(IReadOnlyCollection<LargeTestResult> rows)
		{
			var results = rows.Select(row => new StepLargeTestResultRow
			{
				Quantity1 = Converter.Instance.ToValue<Int32>(row.Quantity1),
				Quantity2 = Converter.Instance.ToValue<Int32>(row.Quantity2),
				Quantity3 = Converter.Instance.ToValue<Int32>(row.Quantity3),
				Quantity4 = Converter.Instance.ToValue<Int32>(row.Quantity4),
				Quantity5 = Converter.Instance.ToValue<Int32>(row.Quantity5),
				Quantity6 = Converter.Instance.ToValue<Int32>(row.Quantity6),
				PurchaseDate = Converter.Instance.ToValue<DateTime>(row.PurchaseDate)
			}).ToList();

			return results;
		}
	}
	// ReSharper restore UnusedAutoPropertyAccessor.Local
	// ReSharper restore BuiltInTypeReferenceStyle
}

using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace PreservedMoose.SpecFlowHarness.FunctionalTests.StepRows
{
	// ReSharper disable BuiltInTypeReferenceStyle
	// ReSharper disable UnusedAutoPropertyAccessor.Local
	[Binding]
	public sealed class StepCalculatorRow : StepRow<StepCalculatorRow>, IEquatable<StepCalculatorRow>
	{
		public enum Function
		{
			None = 0,
			Log,
			Log10,
			Sin
		}

		public Int32 Count { get; private set; }
		public Single Percentage { get; private set; }
		public Decimal Quantity { get; private set; }
		public String Key { get; private set; }

		public bool Equals(StepCalculatorRow other)
		{
			var isEqual = HelperEquals(other);
			return isEqual;
		}

		public static IReadOnlyCollection<StepCalculatorRow> Calculate(IReadOnlyCollection<StepCalculatorRow> rows, Function function)
		{
			IReadOnlyCollection<StepCalculatorRow> resultRows;
			switch (function)
			{
				case Function.Log:
					{
						resultRows = rows.Select(row => new StepCalculatorRow
						{
							Count = (Int32)Math.Log(row.Count),
							Percentage = (Single)Math.Round(Math.Log(row.Percentage), 2),
							Quantity = Decimal.Round((Decimal)Math.Log10((Double)row.Quantity), 4),
							Key = row.Key
						}).ToList();
						break;
					}
				case Function.Log10:
					{
						resultRows = rows.Select(row => new StepCalculatorRow
						{
							Count = (Int32)Math.Log10(row.Count),
							Percentage = (Single)Math.Round(Math.Log(row.Percentage), 2),
							Quantity = Decimal.Round((Decimal)Math.Log10((Double)row.Quantity), 4),
							Key = row.Key
						}).ToList();
						break;
					}
				case Function.Sin:
					{
						resultRows = rows.Select(row => new StepCalculatorRow
						{
							Count = (Int32)Math.Sin(row.Count),
							Percentage = (Single)Math.Round(Math.Sin(row.Percentage), 2),
							Quantity = Decimal.Round((Decimal)Math.Sin((Double)row.Quantity), 4),
							Key = row.Key
						}).ToList();
						break;
					}
				default:
					{
						resultRows = rows.Select(row => new StepCalculatorRow
						{
							Count = row.Count,
							Percentage = row.Percentage,
							Quantity = row.Quantity,
							Key = row.Key
						}).ToList();
						break;
					}
			}
			return resultRows;
		}
	}
	// ReSharper restore UnusedAutoPropertyAccessor.Local
	// ReSharper restore BuiltInTypeReferenceStyle
}

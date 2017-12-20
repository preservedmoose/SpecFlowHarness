using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using PreservedMoose.SpecFlowHarness.TestClasses;

using TechTalk.SpecFlow;

namespace PreservedMoose.SpecFlowHarness.AcceptanceTests.StepRows
{
	// ReSharper disable BuiltInTypeReferenceStyle
	// ReSharper disable UnusedAutoPropertyAccessor.Local
	[Binding]
	public sealed class StepTestResultRow : StepRow<StepTestResultRow>, IEquatable<StepTestResultRow>
	{
		public Boolean IsBooleanValue { get; private set; }
		public Char CharValue { get; private set; }
		public Byte ByteValue { get; private set; }
		public Int16 Int16Value { get; private set; }
		public Int32 Int32Value { get; private set; }
		public Int64 Int64Value { get; private set; }
		public Single SingleValue { get; private set; }
		public Double DoubleValue { get; private set; }
		public Decimal DecimalValue { get; private set; }
		public DateTime DateTimeValue { get; private set; }
		public String StringValue { get; private set; }

		public Colour ColourValue { get; private set; }
		public YearMonth YearMonthValue { get; private set; }
		public CycleType CycleTypeValue { get; private set; }

		public IReadOnlyCollection<Boolean> IsBooleanValues { get; private set; }
		public IReadOnlyCollection<Char> CharValues { get; private set; }
		public IReadOnlyCollection<Byte> ByteValues { get; private set; }
		public IReadOnlyCollection<Int16> Int16Values { get; private set; }
		public IReadOnlyCollection<Int32> Int32Values { get; private set; }
		public IReadOnlyCollection<Int64> Int64Values { get; private set; }
		public IReadOnlyCollection<Single> SingleValues { get; private set; }
		public IReadOnlyCollection<Double> DoubleValues { get; private set; }
		public IReadOnlyCollection<Decimal> DecimalValues { get; private set; }
		public IReadOnlyCollection<DateTime> DateTimeValues { get; private set; }
		public IReadOnlyCollection<String> StringValues { get; private set; }

		public IReadOnlyCollection<Colour> ColourValues { get; private set; }
		public IReadOnlyCollection<YearMonth> YearMonthValues { get; private set; }
		public IReadOnlyCollection<CycleType> CycleTypeValues { get; private set; }

		/// <summary>
		/// default constructor
		/// </summary>
		public StepTestResultRow()
		{
			// values
			IsBooleanValue = false;
			CharValue = ' ';
			ByteValue = 8;
			Int16Value = 16;
			Int32Value = 32;
			Int64Value = 64;
			SingleValue = 1.0f;
			DoubleValue = 2.0f;
			DecimalValue = 10.0m;
			DateTimeValue = new DateTime(2002, 2, 2);
			StringValue = "text";

			// enum
			ColourValue = Colour.Cyan;

			// struct
			YearMonthValue = new YearMonth(2003, 3);

			// class
			CycleTypeValue = CycleType.Timely;

			// lists
			IsBooleanValues = new Collection<Boolean> { true, false };
			CharValues = new Collection<Char> { 'a', 'b' };
			ByteValues = new Collection<Byte> { 1, 2, 3 };
			Int16Values = new Collection<Int16> { 4, 5, 6 };
			Int32Values = new Collection<Int32> { 7, 8, 9 };
			Int64Values = new Collection<Int64> { 10, 11, 12 };
			SingleValues = new Collection<Single> { 10.1f, 11.1f, 12.1f };
			DoubleValues = new Collection<Double> { 10.2d, 11.2d, 12.2d };
			DecimalValues = new Collection<Decimal> { 10.3m, 11.3m, 12.3m };
			DateTimeValues = new Collection<DateTime> { new DateTime(2004, 4, 4), new DateTime(2005, 5, 5) };
			StringValues = new Collection<String> { 1.ToString(), 2.ToString(), 3.ToString() };

			ColourValues = new Collection<Colour> { Colour.Orange, Colour.Purple, Colour.Cyan };
			YearMonthValues = new Collection<YearMonth> { new YearMonth(2006, 6), new YearMonth(2006, 6) };
			CycleTypeValues = new Collection<CycleType> { CycleType.Timely, CycleType.Evening };
		}

		public bool Equals(StepTestResultRow other)
		{
			var isEqual = HelperEquals(other);
			return isEqual;
		}

		public static IReadOnlyCollection<StepTestResultRow> Convert(IReadOnlyCollection<TestResult> rows)
		{
			var results = rows.Select(row => new StepTestResultRow
			{
				IsBooleanValue = Converter.Instance.ToValue<Boolean>(row.IsBooleanValue),
				CharValue = Converter.Instance.ToValue<Char>(row.CharValue),
				ByteValue = Converter.Instance.ToValue<Byte>(row.ByteValue),
				Int16Value = Converter.Instance.ToValue<Int16>(row.Int16Value),
				Int32Value = Converter.Instance.ToValue<Int32>(row.Int32Value),
				Int64Value = Converter.Instance.ToValue<Int64>(row.Int64Value),
				SingleValue = Converter.Instance.ToValue<Single>(row.SingleValue),
				DoubleValue = Converter.Instance.ToValue<Double>(row.DoubleValue),
				DecimalValue = Converter.Instance.ToValue<Decimal>(row.DecimalValue),
				DateTimeValue = Converter.Instance.ToValue<DateTime>(row.DateTimeValue),
				StringValue = Converter.Instance.ToValue<String>(row.StringValue),

				ColourValue = Converter.Instance.ToEnum<Colour>(row.ColourValue),
				YearMonthValue = Converter.Instance.ToObject<YearMonth>(row.YearMonthValue),
				CycleTypeValue = Converter.Instance.ToObjectStatic<CycleType>(row.CycleTypeValue),

				IsBooleanValues = Converter.Instance.ToValues<Boolean>(row.IsBooleanValues),
				CharValues = Converter.Instance.ToValues<Char>(row.CharValues),
				ByteValues = Converter.Instance.ToValues<Byte>(row.ByteValues),
				Int16Values = Converter.Instance.ToValues<Int16>(row.Int16Values),
				Int32Values = Converter.Instance.ToValues<Int32>(row.Int32Values),
				Int64Values = Converter.Instance.ToValues<Int64>(row.Int64Values),
				SingleValues = Converter.Instance.ToValues<Single>(row.SingleValues),
				DoubleValues = Converter.Instance.ToValues<Double>(row.DoubleValues),
				DecimalValues = Converter.Instance.ToValues<Decimal>(row.DecimalValues),
				DateTimeValues = Converter.Instance.ToValues<DateTime>(row.DateTimeValues),
				StringValues = Converter.Instance.ToValues<String>(row.StringValues),

				ColourValues = Converter.Instance.ToEnums<Colour>(row.ColourValues),
				YearMonthValues = Converter.Instance.ToObjects<YearMonth>(row.YearMonthValues),
				CycleTypeValues = Converter.Instance.ToObjectsStatic<CycleType>(row.CycleTypeValues)
			}).ToList();

			return results;
		}
	}
	// ReSharper restore UnusedAutoPropertyAccessor.Local
	// ReSharper restore BuiltInTypeReferenceStyle
}

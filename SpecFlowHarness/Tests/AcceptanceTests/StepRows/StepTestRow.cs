using System;
using System.Collections.Generic;

using PreservedMoose.SpecFlowHarness.TestClasses;

using TechTalk.SpecFlow;

namespace PreservedMoose.SpecFlowHarness.AcceptanceTests.StepRows
{
	// ReSharper disable BuiltInTypeReferenceStyle
	// ReSharper disable UnusedAutoPropertyAccessor.Local
	[Binding]
	public sealed class StepTestRow : StepRow<StepTestRow>
	{
		// values
		public Boolean IsBooleanValue { get; private set; }

		public Char CharValue { get; private set; }

		public Byte ByteValue { get; private set; }
		public Int16 Int16Value { get; private set; }
		public Int32 Int32Value { get; private set; }
		public Int64 Int64Value { get; private set; }

		public SByte SByteValue { get; private set; }
		public UInt16 UInt16Value { get; private set; }
		public UInt32 UInt32Value { get; private set; }
		public UInt64 UInt64Value { get; private set; }

		public Single SingleValue { get; private set; }
		public Double DoubleValue { get; private set; }
		public Decimal DecimalValue { get; private set; }

		public String StringValue { get; private set; }

		public DateTime DateTimeValue { get; private set; }

		// enums
		public Colour ColourValue { get; private set; }

		// structs
		public YearMonth YearMonthValue { get; private set; }

		// classes
		public CycleType CycleTypeValue { get; private set; }

		// value lists
		public IReadOnlyCollection<Int16> Int16Values { get; private set; }
		public IReadOnlyCollection<UInt16> UInt16Values { get; private set; }
		public IReadOnlyCollection<String> StringValues { get; private set; }

		// enum lists
		public IReadOnlyCollection<Colour> ColourValues { get; private set; }

		// struct lists
		public IReadOnlyCollection<YearMonth> YearMonthValues { get; private set; }

		// class lists
		public IReadOnlyCollection<CycleType> CycleTypeValues { get; private set; }

		/// <summary>
		/// default constructor
		/// </summary>
		public StepTestRow()
		{
			StringValue = String.Empty;
			YearMonthValue = YearMonth.MinValue;

			//Int32Value = 1;
			//Int64Value = 1;
			//DecimalValue = 1m;
			//DateTimeValue = DateTime.Now;
			//StringValue = 1.ToString();
		}

		/// <summary>
		/// perform validation on some properties
		/// </summary>
		protected override void Validation()
		{
			// values
			if (IsUsedProperty(s => s.IsBooleanValue)) ValidateValue(IsBooleanValue, s => s.IsBooleanValue);

			if (IsUsedProperty(s => s.CharValue)) ValidateValue(CharValue, s => s.CharValue);

			if (IsUsedProperty(s => s.ByteValue)) ValidateValue(ByteValue, s => s.ByteValue);
			if (IsUsedProperty(s => s.Int16Value)) ValidateValue(Int16Value, s => s.Int16Value);
			if (IsUsedProperty(s => s.Int32Value)) ValidateValue(Int32Value, s => s.Int32Value);
			if (IsUsedProperty(s => s.Int64Value)) ValidateValue(Int64Value, s => s.Int64Value);

			if (IsUsedProperty(s => s.SByteValue)) ValidateValue(SByteValue, s => s.SByteValue);
			if (IsUsedProperty(s => s.UInt16Value)) ValidateValue(UInt16Value, s => s.UInt16Value);
			if (IsUsedProperty(s => s.UInt32Value)) ValidateValue(UInt32Value, s => s.UInt32Value);
			if (IsUsedProperty(s => s.UInt64Value)) ValidateValue(UInt64Value, s => s.UInt64Value);

			if (IsUsedProperty(s => s.SingleValue)) ValidateValue(SingleValue, s => s.SingleValue);
			if (IsUsedProperty(s => s.DoubleValue)) ValidateValue(DoubleValue, s => s.DoubleValue);
			if (IsUsedProperty(s => s.DecimalValue)) ValidateValue(DecimalValue, s => s.DecimalValue);

			if (IsUsedProperty(s => s.DateTimeValue)) ValidateValue(DateTimeValue, s => s.DateTimeValue);

			// enums
			if (IsUsedProperty(s => s.ColourValue)) ValidateEnum(ColourValue, s => s.ColourValue);

			// strings
			if (IsUsedProperty(s => s.StringValue)) ValidateValue(StringValue, s => s.StringValue);
		}
	}
	// ReSharper restore UnusedAutoPropertyAccessor.Local
	// ReSharper restore BuiltInTypeReferenceStyle
}

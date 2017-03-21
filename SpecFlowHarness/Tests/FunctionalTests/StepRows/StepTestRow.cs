using System;
using System.Collections.Generic;
using PreservedMoose.SpecFlowHarness.FunctionalTests.TestClasses;
using TechTalk.SpecFlow;

namespace PreservedMoose.SpecFlowHarness.FunctionalTests.StepRows
{
	// ReSharper disable BuiltInTypeReferenceStyle
	// ReSharper disable UnusedAutoPropertyAccessor.Local
	[Binding]
	public sealed class StepTestRow : StepRow<StepTestRow>
	{
		// values
		public Boolean IsBooleanValue { get; private set; }

		public Byte ByteValue { get; private set; }
		public Char CharValue { get; private set; }
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

		public DateTime DateTimeValue { get; private set; }

		// enums
		public Colour ColourValue { get; private set; }

		// strings
		public String StringValue { get; private set; }

		// structs
		public YearMonth YearMonthValue { get; private set; }

		// classes
		public CycleType CycleTypeValue { get; private set; }

		// lists
		public IReadOnlyCollection<Int16> Int16Values { get; private set; }
		public IReadOnlyCollection<UInt16> UInt16Values { get; private set; }
		public IReadOnlyCollection<Colour> ColourValues { get; private set; }
		public IReadOnlyCollection<String> StringValues { get; private set; }

		public IReadOnlyCollection<YearMonth> YearMonthValues { get; private set; }

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
			//Validate_Int32(Int32Value, s => s.Int32Value);
			//Validate_Int64(Int32Value, s => s.Int32Value);
			//Validate_Decimal(DecimalValue, s => s.DecimalValue);
			//Validate_DateTime(DateTimeValue, s => s.DateTimeValue);
			//Validate_String(StringValue, s => s.StringValue);
		}
	}
	// ReSharper restore UnusedAutoPropertyAccessor.Local
	// ReSharper restore BuiltInTypeReferenceStyle
}

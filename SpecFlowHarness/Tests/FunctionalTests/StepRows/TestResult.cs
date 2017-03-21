using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PreservedMoose.SpecFlowHarness.FunctionalTests.StepRows
{
	public sealed class TestResult
	{
		public string IsBooleanValue { get; set; }
		public string CharValue { get; set; }
		public string ByteValue { get; set; }
		public string Int16Value { get; set; }
		public string Int32Value { get; set; }
		public string Int64Value { get; set; }
		public string SingleValue { get; set; }
		public string DoubleValue { get; set; }
		public string DecimalValue { get; set; }
		public string DateTimeValue { get; set; }
		public string ColourValue { get; set; }
		public string StringValue { get; set; }
		public string YearMonthValue { get; set; }
		public string CycleTypeValue { get; set; }


		public string IsBooleanValues { get; set; }
		public string CharValues { get; set; }
		public string ByteValues { get; set; }
		public string Int16Values { get; set; }
		public string Int32Values { get; set; }
		public string Int64Values { get; set; }
		public string SingleValues { get; set; }
		public string DoubleValues { get; set; }
		public string DecimalValues { get; set; }
		public string DateTimeValues { get; set; }
		public string ColourValues { get; set; }
		public string StringValues { get; set; }
		public string YearMonthValues { get; set; }
		public string CycleTypeValues { get; set; }

		public static IReadOnlyCollection<TestResult> GetTestData()
		{
			var rows = new Collection<TestResult>
			{
				new TestResult
				{
					IsBooleanValue = "yes",
					CharValue = "y",
					ByteValue = "123",
					Int16Value = "1234",
					Int32Value = "12345",
					Int64Value = "123456",
					SingleValue = "1123.45",
					DoubleValue = "2345.67",
					DecimalValue = "32345.67",
					DateTimeValue = "2016-12-27",
					ColourValue = "Red",
					StringValue = "Test 1",
					YearMonthValue = "2016-11",
					CycleTypeValue = "Timely",

					IsBooleanValues = "yes, yes, no",
					CharValues = "a, b, c",
					ByteValues = "121, 122",
					Int16Values = "1231, 1232",
					Int32Values = "12341, 12342",
					Int64Values = "123451, 123452",
					SingleValues = "1123.41, 1123.42",
					DoubleValues = "2345.61, 2345.62",
					DecimalValues = "32345.61, 32345.62",
					DateTimeValues = "2016-12-27, 2016-12-28",
					ColourValues = "Red, Green, Blue",
					StringValues = "Test 1, Test 2",
					YearMonthValues = "2016-11, 2016-10",
					CycleTypeValues = "Timely, Evening"
				},
				new TestResult
				{
					IsBooleanValue = "true",
					CharValue = "t",
					ByteValue = "123",
					Int16Value = "1,234",
					Int32Value = "12,345",
					Int64Value = "123,456",
					SingleValue = "1,123.45",
					DoubleValue = "2,345.67",
					DecimalValue = "32,345.67",
					DateTimeValue = "2016-12-27 13:30",
					ColourValue = "Red Colour",
					StringValue = "Test 2",
					YearMonthValue = "2016-12",
					CycleTypeValue = "Evening",

					IsBooleanValues = "true, true, false",
					CharValues = "a, b, c",
					ByteValues = "121, 122",
					Int16Values = "1,231, 1,232",
					Int32Values = "12,341, 12,342",
					Int64Values = "123,451, 123,452",
					SingleValues = "1,123.41, 1,123.42",
					DoubleValues = "2,345.61, 2,345.62",
					DecimalValues = "32,345.61, 32,345.62",
					DateTimeValues = "2016-12-27 13:30, 2016-12-28 14:30",
					ColourValues = "Red Colour, Green Colour, Blue Colour",
					StringValues = "Test 1, Test 2",
					YearMonthValues = "2016-11, 2016-10",
					CycleTypeValues = "Timely, Evening"
				}
			};
			return rows;
		}
	}
}

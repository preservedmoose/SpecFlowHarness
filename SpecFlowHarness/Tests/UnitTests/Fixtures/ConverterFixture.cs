using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using PreservedMoose.SpecFlowHarness.UnitTests.TestClasses;

namespace PreservedMoose.SpecFlowHarness.UnitTests.Fixtures
{
	// ReSharper disable InconsistentNaming
	[TestFixture]
	public class ConverterFixture : BaseFixture
	{
		public override void OneTimeSetUp()
		{
			base.OneTimeSetUp();

			BaseContainer.Register(typeof(IConverter), typeof(Converter));
		}

		// ---------------------------------------------------------------------------------------------

		[Test]
		public override void Container_should_return_instance_from_interface()
		{
			TestInterface<IConverter, Converter>();
		}

		// ---------------------------------------------------------------------------------------------

		[TestCase(true, "true")]
		[TestCase(false, "false")]
		[TestCase(true, "yes")]
		[TestCase(false, "no")]
		public void ToValue_should_convert_successfully_for_Boolean(Boolean expectedValue, string fromValue)
		{
			// Arrange
			var converter = new Converter();

			// Act
			var actualValue = converter.ToValue<Boolean>(fromValue);

			// Assert
			actualValue.Should().Be(expectedValue);
		}

		[TestCase(1234, "1234")]
		[TestCase(1234, "1,234")]
		public void ToValue_should_convert_successfully_for_Int16(Int16 expectedValue, string fromValue)
		{
			// Arrange
			var converter = new Converter();

			// Act
			var actualValue = converter.ToValue<Int16>(fromValue);

			// Assert
			actualValue.Should().Be(expectedValue);
		}

		[TestCase(1234, "1234")]
		[TestCase(1234, "1,234")]
		public void ToValue_should_convert_successfully_for_Int32(Int32 expectedValue, string fromValue)
		{
			// Arrange
			var converter = new Converter();

			// Act
			var actualValue = converter.ToValue<Int32>(fromValue);

			// Assert
			actualValue.Should().Be(expectedValue);
		}

		[TestCase(1234, "1234")]
		[TestCase(1234, "1,234")]
		public void ToValue_should_convert_successfully_for_Int64(Int64 expectedValue, string fromValue)
		{
			// Arrange
			var converter = new Converter();

			// Act
			var actualValue = converter.ToValue<Int64>(fromValue);

			// Assert
			actualValue.Should().Be(expectedValue);
		}

		[TestCase(2017, 01, 17, 0, 0, 0, 0, "2017-01-17")]
		[TestCase(2017, 01, 17, 10, 55, 0, 0, "2017-01-17 10:55")]
		[TestCase(2017, 01, 17, 10, 55, 33, 0, "2017-01-17 10:55:33")]
		[TestCase(2017, 01, 17, 10, 55, 33, 1, "2017-01-17 10:55:33.001")]
		[TestCase(2017, 01, 17, 10, 55, 33, 11, "2017-01-17 10:55:33.011")]
		[TestCase(2017, 01, 17, 10, 55, 33, 111, "2017-01-17 10:55:33.111")]
		public void ToValue_should_convert_successfully_for_DateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, string fromValue)
		{
			// Arrange
			var expectedValue = new DateTime(year, month, day, hour, minute, second, millisecond);

			var converter = new Converter();

			// Act
			var actualValue = converter.ToValue<DateTime>(fromValue);

			// Assert
			actualValue.Should().Be(expectedValue);
		}

		[Test]
		public void ToValue_should_convert_successfully_for_Single()
		{
			// Arrange
			const string fromValue = "123.45";
			const Single expectedValue = 123.45f;

			var converter = new Converter();

			// Act
			var actualValue = converter.ToValue<Single>(fromValue);

			// Assert
			actualValue.Should().Be(expectedValue);
		}

		[Test]
		public void ToValue_should_convert_successfully_for_Double()
		{
			// Arrange
			const string fromValue = "123.45";
			const Double expectedValue = 123.45d;

			var converter = new Converter();

			// Act
			var actualValue = converter.ToValue<Double>(fromValue);

			// Assert
			actualValue.Should().Be(expectedValue);
		}

		[Test]
		public void ToValue_should_convert_successfully_for_Decimal()
		{
			// Arrange
			const string fromValue = "123.45";
			const Decimal expectedValue = 123.45m;

			var converter = new Converter();

			// Act
			var actualValue = converter.ToValue<Decimal>(fromValue);

			// Assert
			actualValue.Should().Be(expectedValue);
		}

		[Test]
		public void ToValue_should_convert_successfully_for_String()
		{
			// Arrange
			const string fromValue = "one";
			const String expectedValue = "one";

			var converter = new Converter();

			// Act
			var actualValue = converter.ToValue<String>(fromValue);

			// Assert
			actualValue.Should().Be(expectedValue);
		}

		[Test]
		public void ToValue_should_return_with_default_when_no_data()
		{
			// Arrange
			const string fromValue = "";
			const String expectedValue = default(String);

			var converter = new Converter();

			// Act
			var actualValue = converter.ToValue<String>(fromValue);

			// Assert
			actualValue.Should().Be(expectedValue);
		}

		[Test]
		public void ToValue_should_fail_to_convert_with_error_for_Boolean()
		{
			// Arrange
			const string fromValue = "nonsense";

			var converter = new Converter();

			// Act
			Action action = () => converter.ToValue<Boolean>(fromValue);

			// Assert
			action.ShouldThrow<ArgumentException>();
		}

		[Test]
		public void ToValue_should_fail_to_convert_with_error_for_Int16()
		{
			// Arrange
			const string fromValue = "nonsense";

			var converter = new Converter();

			// Act
			Action action = () => converter.ToValue<Int16>(fromValue);

			// Assert
			action.ShouldThrow<ArgumentException>();
		}

		[Test]
		public void ToValue_should_fail_to_convert_with_error_for_Int32()
		{
			// Arrange
			const string fromValue = "nonsense";

			var converter = new Converter();

			// Act
			Action action = () => converter.ToValue<Int32>(fromValue);

			// Assert
			action.ShouldThrow<ArgumentException>();
		}

		[Test]
		public void ToValue_should_fail_to_convert_with_error_for_Int64()
		{
			// Arrange
			const string fromValue = "nonsense";

			var converter = new Converter();

			// Act
			Action action = () => converter.ToValue<Int64>(fromValue);

			// Assert
			action.ShouldThrow<ArgumentException>();
		}

		[Test]
		public void ToValue_should_fail_to_convert_with_error_for_DateTime()
		{
			// Arrange
			const string fromValue = "nonsense";

			var converter = new Converter();

			// Act
			Action action = () => converter.ToValue<DateTime>(fromValue);

			// Assert
			action.ShouldThrow<ArgumentException>();
		}

		[Test]
		public void ToValue_should_fail_to_convert_with_error_for_Single()
		{
			// Arrange
			const string fromValue = "nonsense";

			var converter = new Converter();

			// Act
			Action action = () => converter.ToValue<Single>(fromValue);

			// Assert
			action.ShouldThrow<ArgumentException>();
		}

		[Test]
		public void ToValue_should_fail_to_convert_with_error_for_Double()
		{
			// Arrange
			const string fromValue = "nonsense";

			var converter = new Converter();

			// Act
			Action action = () => converter.ToValue<Double>(fromValue);

			// Assert
			action.ShouldThrow<ArgumentException>();
		}

		[Test]
		public void ToValue_should_fail_to_convert_with_error_for_Decimal()
		{
			// Arrange
			const string fromValue = "nonsense";

			var converter = new Converter();

			// Act
			Action action = () => converter.ToValue<Decimal>(fromValue);

			// Assert
			action.ShouldThrow<ArgumentException>();
		}

		// ---------------------------------------------------------------------------------------------

		[TestCase(Colour.Red, "Red")]
		[TestCase(Colour.Blue, "Blue")]
		[TestCase(Colour.Green, "Green")]
		[TestCase(Colour.Red, "Red Colour")]
		[TestCase(Colour.Blue, "Blue Colour")]
		[TestCase(Colour.Green, "Green Colour")]
		public void ToEnum_should_convert_successfully(Colour expectedValue, string fromValue)
		{
			// Arrange
			var converter = new Converter();

			// Act
			var actualValue = converter.ToEnum<Colour>(fromValue);

			// Assert
			actualValue.Should().Be(expectedValue);
		}

		[Test]
		public void ToEnum_should_return_with_default_when_no_data()
		{
			// Arrange
			const string fromValue = "";
			const Colour expectedValue = default(Colour);

			var converter = new Converter();

			// Act
			var actualValue = converter.ToEnum<Colour>(fromValue);

			// Assert
			actualValue.Should().Be(expectedValue);
		}

		[Test]
		public void ToEnum_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			var converter = new Converter();

			// Act
			Action action = () => converter.ToEnum<Colour>(fromValue);

			// Assert
			action.ShouldThrow<ArgumentException>();
		}

		// ---------------------------------------------------------------------------------------------

		[Test]
		public void ToObject_should_convert_successfully()
		{
			// Arrange
			const string fromValue = "2017-01";

			var expectedValue = new YearMonth(2017, 1);

			var converter = new Converter();

			// Act
			var actualValue = converter.ToObject<YearMonth>(fromValue);

			// Assert
			actualValue.Should().Be(expectedValue);
		}

		[Test]
		public void ToObject_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			var converter = new Converter();

			// Act
			Action action = () => converter.ToObject<YearMonth>(fromValue);

			// Assert
			action.ShouldThrow<ArgumentException>();
		}

		// ---------------------------------------------------------------------------------------------

		[Test]
		public void ToObjectStatic_should_convert_successfully()
		{
			// Arrange
			const string fromValue = "Timely";

			var expectedValue = CycleType.Timely;

			var converter = new Converter();

			// Act
			var actualValue = converter.ToObjectStatic<CycleType>(fromValue);

			// Assert
			actualValue.Should().Be(expectedValue);
		}

		[Test]
		public void ToObjectStatic_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			var converter = new Converter();

			// Act
			Action action = () => converter.ToObjectStatic<CycleType>(fromValue);

			// Assert
			action.ShouldThrow<ArgumentException>();
		}

		// ---------------------------------------------------------------------------------------------

		[Test]
		public void ToValues_should_convert_successfully_for_Boolean()
		{
			// Arrange
			const string fromValues = "true, false, yes, no";

			var expectedValues = new List<Boolean> { true, false, true, false };

			var converter = new Converter();

			// Act
			var actualValues = converter.ToValues<Boolean>(fromValues);

			// Assert
			actualValues.Should().BeEquivalentTo(expectedValues);
		}

		[Test]
		public void ToValues_should_convert_successfully_for_Int16()
		{
			// Arrange
			const string fromValues = "1234, 2345";

			var expectedValues = new List<Int16> { 1234, 2345 };

			var converter = new Converter();

			// Act
			var actualValues = converter.ToValues<Int16>(fromValues);

			// Assert
			actualValues.Should().BeEquivalentTo(expectedValues);
		}

		[Test]
		public void ToValues_should_convert_successfully_for_Int32()
		{
			// Arrange
			const string fromValues = "1234, 2345";

			var expectedValues = new List<Int32> { 1234, 2345 };

			var converter = new Converter();

			// Act
			var actualValues = converter.ToValues<Int32>(fromValues);

			// Assert
			actualValues.Should().BeEquivalentTo(expectedValues);
		}

		[Test]
		public void ToValues_should_convert_successfully_for_Int64()
		{
			// Arrange
			const string fromValues = "1234, 2345";

			var expectedValues = new List<Int64> { 1234L, 2345L };

			var converter = new Converter();

			// Act
			var actualValues = converter.ToValues<Int64>(fromValues);

			// Assert
			actualValues.Should().BeEquivalentTo(expectedValues);
		}

		[Test]
		public void ToValues_should_convert_successfully_for_DateTime()
		{
			// Arrange
			const string fromValues = "2017-01-17, 2017-01-17 10:55, 2017-01-17 10:55:33, 2017-01-17 10:55:33.001, 2017-01-17 10:55:33.011, 2017-01-17 10:55:33.111";

			var expectedValues = new List<DateTime>
			{
				new DateTime(2017, 1, 17, 00, 00, 00, 000),
				new DateTime(2017, 1, 17, 10, 55, 00, 000),
				new DateTime(2017, 1, 17, 10, 55, 33, 000),
				new DateTime(2017, 1, 17, 10, 55, 33, 001),
				new DateTime(2017, 1, 17, 10, 55, 33, 011),
				new DateTime(2017, 1, 17, 10, 55, 33, 111)
			};

			var converter = new Converter();

			// Act
			var actualValues = converter.ToValues<DateTime>(fromValues);

			// Assert
			actualValues.Should().BeEquivalentTo(expectedValues);
		}

		[Test]
		public void ToValues_should_convert_successfully_for_Single()
		{
			// Arrange
			const string fromValues = "123.45, 234.56";

			var expectedValues = new List<Single> { 123.45f, 234.56f };

			var converter = new Converter();

			// Act
			var actualValues = converter.ToValues<Single>(fromValues);

			// Assert
			actualValues.Should().BeEquivalentTo(expectedValues);
		}

		[Test]
		public void ToValues_should_convert_successfully_for_Double()
		{
			// Arrange
			const string fromValues = "123.45, 234.56";

			var expectedValues = new List<Double> { 123.45d, 234.56d };

			var converter = new Converter();

			// Act
			var actualValues = converter.ToValues<Double>(fromValues);

			// Assert
			actualValues.Should().BeEquivalentTo(expectedValues);
		}

		[Test]
		public void ToValues_should_convert_successfully_for_Decimal()
		{
			// Arrange
			const string fromValues = "123.45, 234.56";

			var expectedValues = new List<Decimal> { 123.45m, 234.56m };

			var converter = new Converter();

			// Act
			var actualValues = converter.ToValues<Decimal>(fromValues);

			// Assert
			actualValues.Should().BeEquivalentTo(expectedValues);
		}

		[Test]
		public void ToValues_should_convert_successfully_for_String()
		{
			// Arrange
			const string fromValues = "one, two";

			var expectedValues = new List<String> { "one", "two" };

			var converter = new Converter();

			// Act
			var actualValues = converter.ToValues<String>(fromValues);

			// Assert
			actualValues.Should().BeEquivalentTo(expectedValues);
		}

		[Test]
		public void ToValues_should_return_with_empty_list_when_no_data()
		{
			// Arrange
			const string fromValues = "   ";

			// ReSharper disable once CollectionNeverUpdated.Local
			var expectedValues = new List<String>();

			var converter = new Converter();

			// Act
			var actualValues = converter.ToValues<string>(fromValues);

			// Assert
			actualValues.Should().BeEquivalentTo(expectedValues);
		}

		// ---------------------------------------------------------------------------------------------


	}
	// ReSharper restore InconsistentNaming
}

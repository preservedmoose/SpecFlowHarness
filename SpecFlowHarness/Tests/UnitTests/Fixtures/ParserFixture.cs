using System;

using FluentAssertions;

using PreservedMoose.SpecFlowHarness.TestClasses;

using Xunit;

namespace PreservedMoose.SpecFlowHarness.UnitTests.Fixtures
{
	// ReSharper disable InconsistentNaming
	[Trait("UnitTests", "ParserFixture")]
	public class ParserFixture : BaseFixture, IDisposable
	{
		public ParserFixture()
		{
			BaseContainer.Register(typeof(IParser), typeof(Parser));
		}

		public void Dispose()
		{
			BaseContainer.Dispose();
		}

		// ---------------------------------------------------------------------------------------------

		[Fact]
		public override void Container_should_return_instance_from_interface()
		{
			TestInterface<IParser, Parser>();
		}

		// ---------------------------------------------------------------------------------------------

		[Theory]
		[InlineData("Red Colour", Colour.Red)]
		[InlineData("Blue Colour", Colour.Blue)]
		[InlineData("Green Colour", Colour.Green)]
		public void ReadEnumDescription_should_read_description(string expectedColourName, Colour colour)
		{
			// Arrange
			var parser = new Parser();

			// Act
			var actualColourName = parser.ReadEnumDescription(colour);

			// Assert
			actualColourName.Should().Be(expectedColourName);
		}

		// ---------------------------------------------------------------------------------------------

		[Theory]
		[InlineData(true, "true")]
		[InlineData(false, "false")]
		[InlineData(true, "yes")]
		[InlineData(false, "no")]
		public void ParseBoolean_should_convert_successfully(Boolean expectedValue, string fromValue)
		{
			// Arrange
			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			var actualValue = parser.ParseBoolean(column, fromValue, ref parseError);

			// Assert
			actualValue.Should().Be(expectedValue);
			parseError.Should().Be(string.Empty);
		}

		[Fact]
		public void ParseBoolean_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			parser.ParseBoolean(column, fromValue, ref parseError);

			// Assert
			parseError.Should().NotBe(string.Empty);
		}

		// ---------------------------------------------------------------------------------------------

		[Theory]
		[InlineData(1234, "1234")]
		[InlineData(1234, "1,234")]
		public void ParseInt16_should_convert_successfully(Int16 expectedValue, string fromValue)
		{
			// Arrange
			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			var actualValue = parser.ParseInt16(column, fromValue, ref parseError);

			// Assert
			actualValue.Should().Be(expectedValue);
			parseError.Should().Be(string.Empty);
		}

		[Fact]
		public void ParseInt16_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			parser.ParseInt16(column, fromValue, ref parseError);

			// Assert
			parseError.Should().NotBe(string.Empty);
		}

		// ---------------------------------------------------------------------------------------------

		[Theory]
		[InlineData(1234, "1234")]
		[InlineData(1234, "1,234")]
		public void ParseInt32_should_convert_successfully(Int32 expectedValue, string fromValue)
		{
			// Arrange
			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			var actualValue = parser.ParseInt32(column, fromValue, ref parseError);

			// Assert
			actualValue.Should().Be(expectedValue);
			parseError.Should().Be(string.Empty);
		}

		[Fact]
		public void ParseInt32_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			parser.ParseInt32(column, fromValue, ref parseError);

			// Assert
			parseError.Should().NotBe(string.Empty);
		}

		// ---------------------------------------------------------------------------------------------

		[Theory]
		[InlineData(1234, "1234")]
		[InlineData(1234, "1,234")]
		public void ParseInt64_should_convert_successfully(Int64 expectedValue, string fromValue)
		{
			// Arrange
			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			var actualValue = parser.ParseInt64(column, fromValue, ref parseError);

			// Assert
			actualValue.Should().Be(expectedValue);
			parseError.Should().Be(string.Empty);
		}

		[Fact]
		public void ParseInt64_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			parser.ParseInt64(column, fromValue, ref parseError);

			// Assert
			parseError.Should().NotBe(string.Empty);
		}

		// ---------------------------------------------------------------------------------------------

		[Theory]
		[InlineData(2017, 01, 17, 0, 0, 0, 0, "2017-01-17")]
		[InlineData(2017, 01, 17, 10, 55, 0, 0, "2017-01-17 10:55")]
		[InlineData(2017, 01, 17, 10, 55, 33, 0, "2017-01-17 10:55:33")]
		[InlineData(2017, 01, 17, 10, 55, 33, 1, "2017-01-17 10:55:33.001")]
		[InlineData(2017, 01, 17, 10, 55, 33, 11, "2017-01-17 10:55:33.011")]
		[InlineData(2017, 01, 17, 10, 55, 33, 111, "2017-01-17 10:55:33.111")]
		public void ParseDateTime_should_convert_successfully(int year, int month, int day, int hour, int minute, int second, int millisecond, string fromValue)
		{
			// Arrange
			var column = string.Empty;
			var parseError = string.Empty;

			var expectedValue = new DateTime(year, month, day, hour, minute, second, millisecond);

			var parser = new Parser();

			// Act
			var actualValue = parser.ParseDateTime(column, fromValue, ref parseError);

			// Assert
			actualValue.Should().Be(expectedValue);
			parseError.Should().Be(string.Empty);
		}

		[Fact]
		public void ParseDateTime_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			parser.ParseDateTime(column, fromValue, ref parseError);

			// Assert
			parseError.Should().NotBe(string.Empty);
		}

		// ---------------------------------------------------------------------------------------------

		[Fact]
		public void ParseValue_should_convert_successfully_for_Single()
		{
			// Arrange
			var column = string.Empty;
			var parseError = string.Empty;

			const string fromValue = "123.45";
			const Single expectedValue = 123.45f;

			var parser = new Parser();

			// Act
			var actualValue = (Single)parser.ParseValue(typeof(Single), column, fromValue, ref parseError);

			// Assert
			actualValue.Should().Be(expectedValue);
			parseError.Should().Be(string.Empty);
		}

		[Fact]
		public void ParseValue_should_convert_successfully_for_Double()
		{
			// Arrange
			const string fromValue = "123.45";
			const Double expectedValue = 123.45d;

			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			var actualValue = (Double)parser.ParseValue(typeof(Double), column, fromValue, ref parseError);

			// Assert
			actualValue.Should().Be(expectedValue);
			parseError.Should().Be(string.Empty);
		}

		[Fact]
		public void ParseValue_should_convert_successfully_for_Decimal()
		{
			// Arrange
			const string fromValue = "123.45";
			const Decimal expectedValue = 123.45m;

			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			var actualValue = (Decimal)parser.ParseValue(typeof(Decimal), column, fromValue, ref parseError);

			// Assert
			actualValue.Should().Be(expectedValue);
			parseError.Should().Be(string.Empty);
		}

		[Fact]
		public void ParseValue_should_fail_to_convert_with_invalid_format()
		{
			// Arrange
			const string fromValue = "nonsense";

			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			parser.ParseValue(typeof(Single), column, fromValue, ref parseError);

			// Assert
			parseError.Should().NotBe(string.Empty);
		}

		[Theory]
		[InlineData(typeof(Single), "3.40282347E+39")]
		[InlineData(typeof(Double), "1.7976931348623157E+309")]
		[InlineData(typeof(Decimal), "79,228,162,514,264,337,593,543,950,336")]
		public void ParseValue_should_fail_to_convert_with_overflow(Type type, string fromValue)
		{
			// Arrange
			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			parser.ParseValue(type, column, fromValue, ref parseError);

			// Assert
			parseError.Should().NotBe(string.Empty);
		}

		// ---------------------------------------------------------------------------------------------

		[Theory]
		[InlineData(Colour.Red, "Red")]
		[InlineData(Colour.Blue, "Blue")]
		[InlineData(Colour.Green, "Green")]
		[InlineData(Colour.Red, "Red Colour")]
		[InlineData(Colour.Blue, "Blue Colour")]
		[InlineData(Colour.Green, "Green Colour")]
		public void ParseEnum_should_convert_successfully(Colour expectedValue, string fromValue)
		{
			// Arrange
			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			var actualValue = parser.ParseEnum<Colour>(column, fromValue, ref parseError);

			// Assert
			actualValue.Should().Be(expectedValue);
			parseError.Should().Be(string.Empty);
		}

		[Fact]
		public void ParseEnum_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			parser.ParseEnum<Colour>(column, fromValue, ref parseError);

			// Assert
			parseError.Should().NotBe(string.Empty);
		}

		// ---------------------------------------------------------------------------------------------

		[Fact]
		public void ParseObject_should_convert_successfully()
		{
			// Arrange
			const string fromValue = "2017-01";

			var column = string.Empty;
			var parseError = string.Empty;
			var expectedValue = new YearMonth(2017, 1);

			var parser = new Parser();

			// Act
			var actualValue = parser.ParseObject<YearMonth>(column, fromValue, ref parseError);

			// Assert
			actualValue.Should().Be(expectedValue);
			parseError.Should().Be(string.Empty);
		}

		[Fact]
		public void ParseObject_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			parser.ParseObject<YearMonth>(column, fromValue, ref parseError);

			// Assert
			parseError.Should().NotBe(string.Empty);
		}

		// ---------------------------------------------------------------------------------------------

		[Fact]
		public void ParseObjectStatic_should_convert_successfully()
		{
			// Arrange
			const string fromValue = "Timely";

			var column = string.Empty;
			var parseError = string.Empty;
			var expectedValue = CycleType.Timely;

			var parser = new Parser();

			// Act
			var actualValue = parser.ParseObjectStatic<CycleType>(column, fromValue, ref parseError);

			// Assert
			actualValue.Should().Be(expectedValue);
			parseError.Should().Be(string.Empty);
		}

		[Fact]
		public void ParseObjectStatic_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			var column = string.Empty;
			var parseError = string.Empty;

			var parser = new Parser();

			// Act
			parser.ParseObjectStatic<CycleType>(column, fromValue, ref parseError);

			// Assert
			parseError.Should().NotBe(string.Empty);
		}

		// ---------------------------------------------------------------------------------------------

		[Theory]
		[InlineData(true, "true")]
		[InlineData(false, "false")]
		[InlineData(true, "yes")]
		[InlineData(false, "no")]
		public void TryParseBoolean_should_convert_successfully(Boolean expectedValue, string fromValue)
		{
			// Arrange
			Boolean actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseBoolean(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeTrue();
			actualValue.Should().Be(expectedValue);
		}

		[Fact]
		public void TryParseBoolean_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			Boolean actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseBoolean(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeFalse();
		}

		// ---------------------------------------------------------------------------------------------

		[Theory]
		[InlineData(1234, "1234")]
		[InlineData(1234, "1,234")]
		public void TryParseInt16_should_convert_successfully(Int16 expectedValue, string fromValue)
		{
			// Arrange
			Int16 actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseInt16(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeTrue();
			actualValue.Should().Be(expectedValue);
		}

		[Fact]
		public void TryParseInt16_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			Int16 actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseInt16(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeFalse();
		}

		// ---------------------------------------------------------------------------------------------

		[Theory]
		[InlineData(1234, "1234")]
		[InlineData(1234, "1,234")]
		public void TryParseInt32_should_convert_successfully(Int16 expectedValue, string fromValue)
		{
			// Arrange
			Int32 actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseInt32(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeTrue();
			actualValue.Should().Be(expectedValue);
		}

		[Fact]
		public void TryParseInt32_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			Int32 actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseInt32(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeFalse();
		}

		// ---------------------------------------------------------------------------------------------

		[Theory]
		[InlineData(1234, "1234")]
		[InlineData(1234, "1,234")]
		public void TryParseInt64_should_convert_successfully(Int16 expectedValue, string fromValue)
		{
			// Arrange
			Int64 actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseInt64(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeTrue();
			actualValue.Should().Be(expectedValue);
		}

		[Fact]
		public void TryParseInt64_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			Int64 actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseInt64(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeFalse();
		}

		// ---------------------------------------------------------------------------------------------

		[Theory]
		[InlineData(2017, 01, 17, 0, 0, 0, 0, "2017-01-17")]
		[InlineData(2017, 01, 17, 10, 55, 0, 0, "2017-01-17 10:55")]
		[InlineData(2017, 01, 17, 10, 55, 33, 0, "2017-01-17 10:55:33")]
		[InlineData(2017, 01, 17, 10, 55, 33, 1, "2017-01-17 10:55:33.001")]
		[InlineData(2017, 01, 17, 10, 55, 33, 11, "2017-01-17 10:55:33.011")]
		[InlineData(2017, 01, 17, 10, 55, 33, 111, "2017-01-17 10:55:33.111")]
		public void TryParseDateTime_should_convert_successfully(int year, int month, int day, int hour, int minute, int second, int millisecond, string fromValue)
		{
			// Arrange
			DateTime actualValue;

			var expectedValue = new DateTime(year, month, day, hour, minute, second, millisecond);

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseDateTime(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeTrue();
			actualValue.Should().Be(expectedValue);
		}

		[Fact]
		public void TryParseDateTime_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			DateTime actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseDateTime(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeFalse();
		}

		// ---------------------------------------------------------------------------------------------

		[Fact]
		public void TryParseValue_should_convert_successfully_for_Single()
		{
			// Arrange
			const string fromValue = "123.45";
			const Single expectedValue = 123.45f;

			Single actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseValue(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeTrue();
			actualValue.Should().Be(expectedValue);
		}

		[Fact]
		public void TryParseValue_should_convert_successfully_for_Double()
		{
			// Arrange
			const string fromValue = "123.45";
			const Double expectedValue = 123.45d;

			Double actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseValue(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeTrue();
			actualValue.Should().Be(expectedValue);
		}

		[Fact]
		public void TryParseValue_should_convert_successfully_for_Decimal()
		{
			// Arrange
			const string fromValue = "123.45";
			const Decimal expectedValue = 123.45m;

			Decimal actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseValue(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeTrue();
			actualValue.Should().Be(expectedValue);
		}

		[Fact]
		public void TryParseValue_should_fail_to_convert_with_invalid_format()
		{
			// Arrange
			const string fromValue = "nonsense";

			Single actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseValue(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeFalse();
		}

		[Fact]
		public void TryParseValue_should_fail_to_convert_with_overflow_for_Single()
		{
			// Arrange
			const string fromValue = "3.40282347E+39";

			Single actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseValue(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeFalse();
		}

		[Fact]
		public void TryParseValue_should_fail_to_convert_with_overflow_for_Double()
		{
			// Arrange
			const string fromValue = "1.7976931348623157E+309";

			Double actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseValue(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeFalse();
		}

		[Fact]
		public void TryParseValue_should_fail_to_convert_with_overflow_for_Decimal()
		{
			// Arrange
			const string fromValue = "79,228,162,514,264,337,593,543,950,336";

			Decimal actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseValue(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeFalse();
		}

		// ---------------------------------------------------------------------------------------------

		[Theory]
		[InlineData(Colour.Red, "Red")]
		[InlineData(Colour.Blue, "Blue")]
		[InlineData(Colour.Green, "Green")]
		[InlineData(Colour.Red, "Red Colour")]
		[InlineData(Colour.Blue, "Blue Colour")]
		[InlineData(Colour.Green, "Green Colour")]
		public void TryParseEnum_should_convert_successfully(Colour expectedValue, string fromValue)
		{
			// Arrange
			Colour actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseEnum(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeTrue();
			actualValue.Should().Be(expectedValue);
		}

		[Fact]
		public void TryParseEnum_should_fail_to_convert_with_error()
		{
			// Arrange
			const string fromValue = "nonsense";

			Colour actualValue;

			var parser = new Parser();

			// Act
			var isSuccess = parser.TryParseEnum(fromValue, out actualValue);

			// Assert
			isSuccess.Should().BeFalse();
		}

		// ---------------------------------------------------------------------------------------------
	}
	// ReSharper restore InconsistentNaming
}

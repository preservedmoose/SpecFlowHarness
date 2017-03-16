using System;

namespace CIAndT.SpecFlowHarness
{
	/// <summary>
	/// This class is slightly different from the Converter class
	/// It is specifically for the use of the transformation logic in the StepRow class
	/// </summary>
	public interface IParser
	{
		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// returns the value in the description attribute for the Enum value
		/// </summary>
		/// <param name="enumType"></param>
		/// <returns></returns>
		string ReadEnumDescription<TEnum>(TEnum enumType) where TEnum : struct;

		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// helper to allow yes/no values
		/// </summary>
		/// <param name="column"></param>
		/// <param name="fromValue"></param>
		/// <param name="parseError"></param>
		/// <returns></returns>
		bool ParseBoolean(string column, string fromValue, ref string parseError);

		/// <summary>
		/// helper to allow comma separated values
		/// </summary>
		/// <param name="column"></param>
		/// <param name="fromValue"></param>
		/// <param name="parseError"></param>
		/// <returns></returns>
		short ParseInt16(string column, string fromValue, ref string parseError);

		/// <summary>
		/// helper to allow comma separated values
		/// </summary>
		/// <param name="column"></param>
		/// <param name="fromValue"></param>
		/// <param name="parseError"></param>
		/// <returns></returns>
		int ParseInt32(string column, string fromValue, ref string parseError);

		/// <summary>
		/// helper to allow comma separated values
		/// </summary>
		/// <param name="column"></param>
		/// <param name="fromValue"></param>
		/// <param name="parseError"></param>
		/// <returns></returns>
		long ParseInt64(string column, string fromValue, ref string parseError);

		/// <summary>
		/// helper to handle dates in the passed in value (two formats supported)
		/// </summary>
		/// <param name="column"></param>
		/// <param name="fromValue"></param>
		/// <param name="parseError"></param>
		/// <returns></returns>
		DateTime ParseDateTime(string column, string fromValue, ref string parseError);

		/// <summary>
		/// helper to parse value types (float, double, decimal...)
		/// </summary>
		/// <param name="column"></param>
		/// <param name="fromValue"></param>
		/// <param name="type"></param>
		/// <param name="parseError"></param>
		/// <returns></returns>
		object ParseValue(Type type, string column, string fromValue, ref string parseError);

		/// <summary>
		/// helper to parse Enum types
		/// </summary>
		/// <typeparam name="TEnum">the enum type</typeparam>
		/// <param name="column">the column name displayed in any error</param>
		/// <param name="fromValue">what we wish to parse</param>
		/// <param name="parseError">the error string returned if the conversion failed</param>
		/// <returns></returns>
		TEnum ParseEnum<TEnum>(string column, string fromValue, ref string parseError) where TEnum : struct;

		/// <summary>
		/// helper to parse object values
		/// </summary>
		/// <param name="column"></param>
		/// <param name="fromValue"></param>
		/// <param name="parseError"></param>
		/// <returns></returns>
		TObject ParseObject<TObject>(string column, string fromValue, ref string parseError) where TObject : IConvertibleFromString<TObject>, new();

		/// <summary>
		/// helper to parse object values
		/// </summary>
		/// <param name="column"></param>
		/// <param name="fromValue"></param>
		/// <param name="parseError"></param>
		/// <returns></returns>
		TObject ParseObjectStatic<TObject>(string column, string fromValue, ref string parseError) where TObject : IConvertibleStaticFromString<TObject>, new();

		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// Tries to parse to a Boolean, handling case insensitive 'Yes' and 'No'.
		/// </summary>
		/// <param name="fromValue">From value.</param>
		/// <param name="toValue">The return value.</param>
		/// <returns></returns>
		bool TryParseBoolean(string fromValue, out bool toValue);

		/// <summary>
		/// Tries to parse an Int16, allowing for the thousands separator.
		/// </summary>
		/// <param name="fromValue">From value.</param>
		/// <param name="toValue">The return value.</param>
		/// <returns></returns>
		bool TryParseInt16(string fromValue, out short toValue);

		/// <summary>
		/// Tries to parse an Int32, allowing for the thousands separator.
		/// </summary>
		/// <param name="fromValue">From value.</param>
		/// <param name="toValue">The return value.</param>
		/// <returns></returns>
		bool TryParseInt32(string fromValue, out int toValue);

		/// <summary>
		/// Tries to parse an Int64, allowing for the thousands separator.
		/// </summary>
		/// <param name="fromValue">From value.</param>
		/// <param name="toValue">The return value.</param>
		/// <returns></returns>
		bool TryParseInt64(string fromValue, out long toValue);

		/// <summary>
		/// Tries to parse a DateTime, allowing for three ISO formats from the Constants class.
		/// </summary>
		/// <param name="fromValue">From value.</param>
		/// <param name="toValue">The return value.</param>
		/// <returns></returns>
		bool TryParseDateTime(string fromValue, out DateTime toValue);

		/// <summary>
		/// Tries to parse for other value types
		/// uint, float, string... etc
		/// </summary>
		/// <typeparam name="TValue">The type of the c.</typeparam>
		/// <param name="fromValue">The s value.</param>
		/// <param name="toValue">The return value.</param>
		/// <returns></returns>
		bool TryParseValue<TValue>(string fromValue, out TValue toValue); //where TValue : ValueType

		/// <summary>
		/// Tries the parse enum.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="fromValue">From value.</param>
		/// <param name="toValue">To value.</param>
		/// <returns></returns>
		bool TryParseEnum<TEnum>(string fromValue, out TEnum toValue) where TEnum : struct;
	}

	// ----------------------------------------------------------------------------------------
}

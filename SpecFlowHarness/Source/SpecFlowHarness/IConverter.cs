using System;
using System.Collections.Generic;

namespace PreservedMoose.SpecFlowHarness
{
	/// <summary>
	/// These methods all take a string and attempt to return the expected type.
	/// If the string is null or empty they will return the requested type (but empty).
	/// Otherwise they will try and convert the string and, if they fail, will throw an ArgumentException exception.
	/// </summary>
	public interface IConverter
	{
		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// helper to parse Value type
		/// There are custom overrides for these extra options:
		/// Boolean  - case insensitive 'Yes' and 'No'
		/// Int16    - the thousands separator
		/// Int32    - the thousands separator
		/// Int64    - the thousands separator
		/// DateTime - three ISO formats from the Constants class
		/// </summary>
		/// <typeparam name="TValue">the value type</typeparam>
		/// <param name="fromValue"></param>
		/// <returns></returns>
		TValue ToValue<TValue>(String fromValue);
		//where TValue : ValueType

		/// <summary>
		/// helper to parse Enum types
		/// </summary>
		/// <typeparam name="TEnum">the enum type</typeparam>
		/// <param name="fromValue">what we wish to parse</param>
		/// <returns></returns>
		TEnum ToEnum<TEnum>(String fromValue)
			where TEnum : struct;

		/// <summary>
		/// helper to parse object values
		/// </summary>
		/// <typeparam name="TObject">the object type</typeparam>
		/// <param name="fromValue"></param>
		/// <returns></returns>
		TObject ToObject<TObject>(String fromValue)
			where TObject : IConvertibleFromString<TObject>, new();

		/// <summary>
		/// helper to parse static object values
		/// </summary>
		/// <typeparam name="TObject">the static object type</typeparam>
		/// <param name="fromValue"></param>
		/// <returns></returns>
		TObject ToObjectStatic<TObject>(String fromValue)
			where TObject : IConvertibleStaticFromString<TObject>, new();

		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// helper to handle dates in the passed in value using the passed in format string
		/// sometimes useful when it is necessary to use formats outside of those supported by ToValue
		/// </summary>
		/// <param name="fromValue"></param>
		/// <param name="sFormat"></param>
		/// <returns></returns>
		DateTime ToDateTime(String fromValue, String sFormat);

		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// helper to parse value types
		/// </summary>
		/// <typeparam name="TValue">the value type</typeparam>
		/// <param name="fromValues">a comma separated list that we wish to parse</param>
		/// <returns></returns>
		IReadOnlyList<TValue> ToValues<TValue>(String fromValues);
		//where TValue : ValueType

		/// <summary>
		/// helper to parse Enum types
		/// </summary>
		/// <typeparam name="TEnum">the enum type</typeparam>
		/// <param name="fromValues">a comma separated list that we wish to parse</param>
		/// <returns></returns>
		IReadOnlyList<TEnum> ToEnums<TEnum>(String fromValues)
			where TEnum : struct;

		/// <summary>
		/// helper to parse object types
		/// </summary>
		/// <typeparam name="TObject">the object type</typeparam>
		/// <param name="fromValues">a comma separated list that we wish to parse</param>
		/// <returns></returns>
		IReadOnlyList<TObject> ToObjects<TObject>(String fromValues)
			where TObject : IConvertibleFromString<TObject>, new();

		/// <summary>
		/// helper to parse static object types
		/// </summary>
		/// <typeparam name="TObject">the static object type</typeparam>
		/// <param name="fromValues">a comma separated list that we wish to parse</param>
		/// <returns></returns>
		IReadOnlyList<TObject> ToObjectsStatic<TObject>(String fromValues)
			where TObject : IConvertibleStaticFromString<TObject>, new();

		// ----------------------------------------------------------------------------------------
	}
}

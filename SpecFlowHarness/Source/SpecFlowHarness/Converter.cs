using System;
using System.Collections.Generic;
using System.Globalization;

namespace PreservedMoose.SpecFlowHarness
{
	public class Converter : IConverter
	{
		public static IConverter Instance { get; } = new Converter();

		private readonly IParser _parser;

		// ----------------------------------------------------------------------------------------

		public Converter()
		{
			_parser = new Parser();
		}

		// ----------------------------------------------------------------------------------------

		public TValue ToValue<TValue>(String fromValue)
			//where TValue : ValueType
		{
			var type = typeof(TValue);
			dynamic toValue = default(TValue);

			var sValue = ToCleanString(fromValue);
			if (sValue == String.Empty) return toValue;

			// ReSharper disable once RedundantAssignment
			var isValid = false;

			// the per-type methods handle custom parsing
			if (type == typeof(Boolean))
			{
				Boolean value;
				isValid = _parser.TryParseBoolean(sValue, out value);
				if (isValid) toValue = value;
			}
			else if (type == typeof(Int16))
			{
				Int16 value;
				isValid = _parser.TryParseInt16(sValue, out value);
				if (isValid) toValue = value;
			}
			else if (type == typeof(Int32))
			{
				Int32 value;
				isValid = _parser.TryParseInt32(sValue, out value);
				if (isValid) toValue = value;
			}
			else if (type == typeof(Int64))
			{
				Int64 value;
				isValid = _parser.TryParseInt64(sValue, out value);
				if (isValid) toValue = value;
			}
			else if (type == typeof(DateTime))
			{
				DateTime value;
				isValid = _parser.TryParseDateTime(sValue, out value);
				if (isValid) toValue = value;
			}
			else    // just parse it normally
			{
				TValue value;
				isValid = _parser.TryParseValue(sValue, out value);
				if (isValid) toValue = value;
			}

			if (!isValid)
			{
				throw new ArgumentException(String.Format(Resources.Converter_InvalidValue, sValue, type));
			}
			return toValue;
		}

		public TEnum ToEnum<TEnum>(String fromValue)
			where TEnum : struct
		{
			var type = typeof(TEnum);
			var toEnum = default(TEnum);

			var sValue = ToCleanString(fromValue);
			if (sValue == String.Empty) return toEnum;

			var isValid = Enum.TryParse(sValue, true, out toEnum);

			if (!isValid)
			{
				// try and look at the descriptions (if they exist)
				foreach (TEnum enumNext in Enum.GetValues(typeof(TEnum)))
				{
					if (0 != String.Compare(_parser.ReadEnumDescription(enumNext), sValue, true, CultureInfo.InvariantCulture)) continue;

					toEnum = enumNext;
					isValid = true;
					break;
				}
			}

			if (!isValid)
			{
				throw new ArgumentException(String.Format(Resources.Converter_InvalidValue, sValue, type));
			}
			return toEnum;
		}

		public TObject ToObject<TObject>(String fromValue)
			where TObject : IConvertibleFromString<TObject>, new()
		{
			var type = typeof(TObject);
			var toObject = new TObject();

			var sValue = ToCleanString(fromValue);
			if (sValue == String.Empty) return toObject;

			var isValid = toObject.TryParse(sValue);

			if (!isValid)
			{
				throw new ArgumentException(String.Format(Resources.Converter_InvalidValue, sValue, type));
			}
			return toObject;
		}

		public TObject ToObjectStatic<TObject>(String fromValue)
			where TObject : IConvertibleStaticFromString<TObject>, new()
		{
			// for calls that return static objects
			// we must create an instance on which to call the method
			// and then return the static object, not the one created
			var type = typeof(TObject);
			var toObject = new TObject();

			var sValue = ToCleanString(fromValue);
			if (sValue == String.Empty) return toObject;

			var temporary = new TObject();
			var isValid = temporary.TryParse(sValue, out toObject);

			if (!isValid)
			{
				throw new ArgumentException(String.Format(Resources.Converter_InvalidValue, sValue, type));
			}
			return toObject;
		}

		// ----------------------------------------------------------------------------------------

		public DateTime ToDateTime(String fromValue, String format)
		{
			var type = typeof(DateTime);
			var toDateTime = default(DateTime);

			var sValue = ToCleanString(fromValue);
			if (sValue == String.Empty) return toDateTime;

			var isValid = DateTime.TryParseExact(sValue, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out toDateTime);

			if (!isValid)
			{
				throw new ArgumentException(String.Format(Resources.Converter_InvalidValue, sValue, type));
			}
			return toDateTime;
		}

		// ----------------------------------------------------------------------------------------

		public IReadOnlyList<TValue> ToValues<TValue>(String fromValues)
			//where TValue : ValueType
		{
			var toValues = new List<TValue>();

			var sValues = ToCleanString(fromValues);
			if (sValues == String.Empty) return toValues;

			var valuesArray = sValues.Split(new[] { Constants.CommaSeparator }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var value in valuesArray)
			{
				toValues.Add(ToValue<TValue>(value));
			}
			return toValues;
		}

		public IReadOnlyList<TEnum> ToEnums<TEnum>(String fromValues)
			where TEnum : struct
		{
			var toEnums = new List<TEnum>();

			var sValues = ToCleanString(fromValues);
			if (sValues == String.Empty) return toEnums;

			var valuesArray = sValues.Split(new[] { Constants.CommaSeparator }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var value in valuesArray)
			{
				toEnums.Add(ToEnum<TEnum>(value));
			}
			return toEnums;
		}

		public IReadOnlyList<TObject> ToObjects<TObject>(String fromValues)
			where TObject : IConvertibleFromString<TObject>, new()
		{
			var toObjects = new List<TObject>();

			var sValues = ToCleanString(fromValues);
			if (sValues == String.Empty) return toObjects;

			var valuesArray = sValues.Split(new[] { Constants.CommaSeparator }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var value in valuesArray)
			{
				toObjects.Add(ToObject<TObject>(value));
			}
			return toObjects;
		}

		public IReadOnlyList<TObject> ToObjectsStatic<TObject>(String fromValues)
			where TObject : IConvertibleStaticFromString<TObject>, new()
		{
			var toObjects = new List<TObject>();

			var sValues = ToCleanString(fromValues);
			if (sValues == String.Empty) return toObjects;

			var valuesArray = sValues.Split(new[] { Constants.CommaSeparator }, StringSplitOptions.RemoveEmptyEntries);

			foreach (var value in valuesArray)
			{
				toObjects.Add(ToObjectStatic<TObject>(value));
			}
			return toObjects;
		}

		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// To a compact String, removing spaces.
		/// </summary>
		/// <param name="fromValue">From value.</param>
		/// <param name="removeSpacing">Remove spaces inside the text.</param>
		/// <returns></returns>
		private static String ToCleanString(String fromValue, Boolean removeSpacing = false)
		{
			var toValue = fromValue?.Trim() ?? String.Empty;
			if (removeSpacing) toValue = toValue.Replace(Constants.Space, String.Empty);
			return toValue;
		}

		// ----------------------------------------------------------------------------------------
	}
}

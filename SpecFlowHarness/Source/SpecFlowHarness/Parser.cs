using System;
using System.ComponentModel;
using System.Globalization;

namespace PreservedMoose.SpecFlowHarness
{
	public class Parser : IParser
	{
		// ----------------------------------------------------------------------------------------

		public string ReadEnumDescription<TEnum>(TEnum enumType)
			where TEnum : struct
		{
			var fieldInfo = enumType.GetType().GetField(enumType.ToString());
			var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

			var description = (attributes.Length > 0) ? attributes[0].Description : typeof(TEnum).ToString();
			return description;
		}

		// ----------------------------------------------------------------------------------------

		public bool ParseBoolean(string column, string fromValue, ref string parseError)
		{
			bool value;
			var isSuccess = TryParseBoolean(fromValue, out value);
			if (!isSuccess)
			{
				parseError = string.Format(Resources.Parser_InvalidValue, column, fromValue, typeof(bool));
			}
			return value;
		}

		public short ParseInt16(string column, string fromValue, ref string parseError)
		{
			short value;
			var isSuccess = TryParseInt16(fromValue, out value);
			if (!isSuccess)
			{
				parseError = string.Format(Resources.Parser_InvalidValue, column, fromValue, typeof(short));
			}
			return value;
		}

		public int ParseInt32(string column, string fromValue, ref string parseError)
		{
			int value;
			var isSuccess = TryParseInt32(fromValue, out value);
			if (!isSuccess)
			{
				parseError = string.Format(Resources.Parser_InvalidValue, column, fromValue, typeof(int));
			}
			return value;
		}

		public long ParseInt64(string column, string fromValue, ref string parseError)
		{
			long value;
			var isSuccess = TryParseInt64(fromValue, out value);
			if (!isSuccess)
			{
				parseError = string.Format(Resources.Parser_InvalidValue, column, fromValue, typeof(long));
			}
			return value;
		}

		public DateTime ParseDateTime(string column, string fromValue, ref string parseError)
		{
			DateTime value;
			var isSuccess = TryParseDateTime(fromValue, out value);
			if (!isSuccess)
			{
				parseError = string.Format(Resources.Parser_InvalidValue, column, fromValue, typeof(DateTime));
			}
			return value;
		}

		public object ParseValue(Type type, string column, string fromValue, ref string parseError)
		{
			ValueType value = null;
			var exceptionThrown = false;

			try
			{
				value = (ValueType)Convert.ChangeType(fromValue, type);
			}
			catch (FormatException)
			{
				exceptionThrown = true;
			}
			catch (OverflowException)
			{
				exceptionThrown = true;
			}

			if (exceptionThrown)
			{
				parseError = string.Format(Resources.Parser_InvalidValue, column, fromValue, type);
			}
			return value;
		}

		public TEnum ParseEnum<TEnum>(string column, string fromValue, ref string parseError)
			where TEnum : struct
		{
			TEnum value;
			var isSuccess = TryParseEnum(fromValue, out value);
			if (!isSuccess)
			{
				parseError = string.Format(Resources.Parser_InvalidValue, column, fromValue, typeof(TEnum));
			}
			return value;
		}

		public TObject ParseObject<TObject>(string column, string fromValue, ref string parseError)
			where TObject : IConvertibleFromString<TObject>, new()
		{
			var value = new TObject();
			if (value.TryParse(fromValue)) return value;

			parseError = string.Format(Resources.Parser_InvalidValue, column, fromValue, typeof(TObject));
			return value;
		}

		public TObject ParseObjectStatic<TObject>(string column, string fromValue, ref string parseError)
			where TObject : IConvertibleStaticFromString<TObject>, new()
		{
			// for calls that return static objects
			// we must create an instance on which to call the method
			// and then return the static object, not the one created
			TObject value;
			var temporary = new TObject();
			if (temporary.TryParse(fromValue, out value)) return value;

			parseError = string.Format(Resources.Parser_InvalidValue, column, fromValue, typeof(TObject));
			return value;
		}

		// ----------------------------------------------------------------------------------------

		public bool TryParseBoolean(string fromValue, out bool toValue)
		{
			var isSuccess = bool.TryParse(fromValue, out toValue);

			// allow yes/no values
			if (!isSuccess)
			{
				if (0 == string.Compare(fromValue, Resources.Parser_No, true, CultureInfo.InvariantCulture) ||
					0 == string.Compare(fromValue, Resources.Parser_Yes, true, CultureInfo.InvariantCulture))
				{
					toValue = (0 == string.Compare(fromValue, Resources.Parser_Yes, true, CultureInfo.InvariantCulture));
					isSuccess = true;
				}
			}
			return isSuccess;
		}

		public bool TryParseInt16(string fromValue, out short toValue)
		{
			// allow comma separated values
			var isSuccess = short.TryParse(fromValue, NumberStyles.Integer | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out toValue);
			return isSuccess;
		}

		public bool TryParseInt32(string fromValue, out int toValue)
		{
			// allow comma separated values
			var isSuccess = int.TryParse(fromValue, NumberStyles.Integer | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out toValue);
			return isSuccess;
		}

		public bool TryParseInt64(string fromValue, out long toValue)
		{
			// allow comma separated values
			var isSuccess = long.TryParse(fromValue, NumberStyles.Integer | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out toValue);
			return isSuccess;
		}

		public bool TryParseDateTime(string fromValue, out DateTime toValue)
		{
			// we are only interested in ISO 8601 based dates
			var isSuccess =
				DateTime.TryParseExact(fromValue, Constants.IsoDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out toValue) ||
				DateTime.TryParseExact(fromValue, Constants.IsoDateTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out toValue) ||
				DateTime.TryParseExact(fromValue, Constants.IsoDateTimePrecisionMinutes, CultureInfo.InvariantCulture, DateTimeStyles.None, out toValue) ||
				DateTime.TryParseExact(fromValue, Constants.IsoDateTimePrecisionMilliseconds, CultureInfo.InvariantCulture, DateTimeStyles.None, out toValue);

			return isSuccess;
		}

		public bool TryParseValue<TValue>(string fromValue, out TValue toValue)
		{
			var isSuccess = true;
			toValue = default(TValue);
			try
			{
				// use the build-in converter for the others
				toValue = (TValue)Convert.ChangeType(fromValue, typeof(TValue), CultureInfo.InvariantCulture);
			}
			catch (FormatException)
			{
				isSuccess = false;
			}
			catch (OverflowException)
			{
				isSuccess = false;
			}
			return isSuccess;
		}

		public bool TryParseEnum<TEnum>(string fromValue, out TEnum toValue)
			where TEnum : struct
		{
			var sValueNoSpaces = fromValue.Replace(Constants.Space, string.Empty);

			// try the built in parser first
			var isSuccess = Enum.TryParse(sValueNoSpaces, true, out toValue);
			// ReSharper disable once InvertIf
			if (!isSuccess)
			{
				// try and look at the descriptions (if they exist)
				foreach (TEnum enumNext in Enum.GetValues(typeof(TEnum)))
				{
					if (0 != string.Compare(ReadEnumDescription(enumNext), fromValue, true, CultureInfo.InvariantCulture)) continue;

					toValue = enumNext;
					isSuccess = true;
					break;
				}
			}
			return isSuccess;
		}

		// ----------------------------------------------------------------------------------------

	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CIAndT.SpecFlowHarness.FunctionalTests.TestClasses
{
	[Serializable]
	public struct YearMonth : IEquatable<YearMonth>, IComparable<YearMonth>, IConvertibleFromString<YearMonth>
	{
		private const short Offset = 100;

		public static readonly string YyyyMm = "yyyyMM";
		public static readonly YearMonth MinValue = new YearMonth(DateTime.MinValue.Year, DateTime.MinValue.Month);
		public static readonly YearMonth MaxValue = new YearMonth(DateTime.MaxValue.Year, DateTime.MaxValue.Month);

		public static YearMonth Now
		{
			get
			{
				var now = DateTime.UtcNow;
				return new YearMonth((short)now.Year, (short)now.Month);
			}
		}

		public short Year => (short)(Value / Offset);

		public short Month => (short)(Value % Offset);

		public int Value { get; private set; }

		#region Constructors

		/// <summary>
		/// 
		/// </summary>
		/// <param name="yearMonth"></param>
		/// <exception cref="ArgumentException"></exception>
		public YearMonth(int yearMonth)
			: this()
		{
			Initialise(yearMonth);
		}

		/// <summary>
		/// Creates a YearMonth from numeric representations of year and month
		/// </summary>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <exception cref="ArgumentException"></exception>
		/// [Obsolete]
		public YearMonth(int year, int month)
			: this()
		{
			Initialise((short)year, (short)month);
		}

		/// <summary>
		/// Creates a YearMonth from numeric representations of year and month
		/// </summary>
		/// <param name="year"></param>
		/// <param name="month"></param>
		/// <exception cref="ArgumentException"></exception>
		public YearMonth(short year, short month)
			: this()
		{
			Initialise(year, month);
		}

		/// <summary>
		/// Creates a YearMonth from a date (only year and month considered)
		/// </summary>
		/// <param name="dateTime"></param>
		/// <exception cref="ArgumentException"></exception>
		public YearMonth(DateTime dateTime)
			: this()
		{
			Initialise((short)dateTime.Year, (short)dateTime.Month);
		}

		/// <summary>
		/// Creates a YearMonth from a string
		/// </summary>
		/// <param name="yearMonth">Should be formatted as  MM/yyyy, yyyy/MM or yyyy-MM</param>
		/// <exception cref="ArgumentException"></exception>
		public YearMonth(string yearMonth)
			: this()
		{
			Initialise(yearMonth);
		}

		#endregion

		/// <summary>
		/// Checks if integer is formated as yyyyMM
		/// </summary>
		/// <param name="yearMonth"></param>
		/// <returns></returns>
		public static bool IsValidMonthYear(int yearMonth)
		{
			var isValid = true;
			var yearMonthStr = yearMonth.ToString();
			if (yearMonthStr.Length != 6)
			{
				isValid = false;
			}
			else
			{
				var month = GetMonth(yearMonthStr);
				if (month < 1 || month > 12)
				{
					isValid = false;
				}
			}
			return isValid;
		}

		public static int GetMonth(string yearMonthStr)
		{
			ValidateDate(yearMonthStr);

			var month = Convert.ToInt32(yearMonthStr.Substring(4, 2));
			return month;
		}

		public static int ExtractYear(int yearMonth)
		{
			var yearMonthStr = yearMonth.ToString(CultureInfo.InvariantCulture);
			ValidateDate(yearMonthStr);

			var year = Convert.ToInt32(yearMonthStr.Substring(0, 4));
			return year;
		}

		public static int ExtractMonth(int yearMonth)
		{
			var yearMonthStr = yearMonth.ToString(CultureInfo.InvariantCulture);
			ValidateDate(yearMonthStr);

			var month = Convert.ToInt32(yearMonthStr.Substring(4, 2));
			return month;
		}

		// ReSharper disable once UnusedParameter.Local
		private static void ValidateDate(string yearMonth)
		{
			if (string.IsNullOrEmpty(yearMonth))
			{
				throw new ArgumentException("Parameter cannot be null or empty");
			}

			if (yearMonth.Length != 6)
			{
				throw new ArgumentException("Parameter must have 6 characters.");
			}
		}

		/// <summary>
		/// Checks if integer is formated as MM/yyyy, yyyy/MM or yyyy-MM
		/// </summary>
		/// <param name="yearMonth"></param>
		/// <returns></returns>
		public static bool IsValidMonthYear(string yearMonth)
		{
			// perform some basic validation
			yearMonth = yearMonth ?? string.Empty;
			if (yearMonth.Length != 7) return false;
			if (!yearMonth.Contains('/') && !yearMonth.Contains('-')) return false;

			// split the string in two using one of the delimiters
			var split = yearMonth.Split('/');
			if (1 == split.Length) split = yearMonth.Split('-');
			if (2 != split.Length) return false;

			// avoid throwing an exception
			int split0;
			int split1;
			if (!int.TryParse(split[0], out split0)) return false;
			if (!int.TryParse(split[1], out split1)) return false;

			// validate the month and year
			var year = (2 == split[0].Length) ? split1 : split0;
			var month = (2 == split[0].Length) ? split0 : split1;

			var isValid = (month >= 1 && month <= 12 && year >= 0 && year <= 9999);
			return isValid;
		}

		public static int DaysInYear(int year)
		{
			var days = (DateTime.IsLeapYear(year)) ? 366 : 365;
			return days;
		}

		#region Initialisers

		private void Initialise(int yearMonth)
		{
			if (!IsValidMonthYear(yearMonth))
			{
				throw new ArgumentException("YearMonth must be formatted as: " + YyyyMm);
			}
			var month = (short)ExtractMonth(yearMonth);
			var year = (short)ExtractYear(yearMonth);
			Value = (year * Offset) + month;
		}

		private void Initialise(string yearMonth)
		{
			if (!IsValidMonthYear(yearMonth))
			{
				throw new ArgumentException("YearMonth should be formatted as MM/yyyy, yyyy/MM or yyyy-MM");
			}

			var split = yearMonth.Split('/');
			if (1 == split.Length) split = yearMonth.Split('-');

			var year = (2 == split[0].Length) ? short.Parse(split[1]) : short.Parse(split[0]);
			var month = (2 == split[0].Length) ? short.Parse(split[0]) : short.Parse(split[1]);

			Initialise(year, month);
		}

		private void Initialise(short year, short month)
		{
			if (!(year >= DateTime.MinValue.Year && year <= DateTime.MaxValue.Year))
			{
				throw new ArgumentException(string.Format("Year must be between {0} and {1}.", DateTime.MinValue.Year, DateTime.MaxValue.Year), "year");
			}

			if (!(month > 0 && month <= 12))
			{
				throw new ArgumentException(@"Month must be between 1 and 12.", "month");
			}
			Value = (year * Offset) + month;
		}

		#endregion

		/// <summary>
		/// method to parse string to object for ITryParseConverter
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public bool TryParse(string value)
		{
			if (!IsValidMonthYear(value)) return false;

			var split = value.Split('/');
			if (1 == split.Length) split = value.Split('-');

			var year = (2 == split[0].Length) ? short.Parse(split[1]) : short.Parse(split[0]);
			var month = (2 == split[0].Length) ? short.Parse(split[0]) : short.Parse(split[1]);

			try
			{
				Initialise(year, month);
			}
			catch (ArgumentException)
			{
				return false;
			}
			return true;
		}

		#region Comparisons

		/// <summary>
		/// Compares the (typed) structs for equality.
		/// </summary>
		/// <param name="other">The other struct to compare against.</param>
		/// <returns></returns>
		public bool Equals(YearMonth other)
		{
			var isEqual = Value.Equals(other.Value);
			return isEqual;
		}

		/// <summary>
		/// Determines whether the specified object is equal to this instance.
		/// </summary>
		/// <param name="obj">The object to compare with this instance.</param>
		public override bool Equals(object obj)
		{
			if (!(obj is YearMonth)) return false;
			var isEqual = Equals((YearMonth)obj);
			return isEqual;
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer that is the hash code for this instance.
		/// </returns>
		public override int GetHashCode()
		{
			// ReSharper disable once NonReadonlyMemberInGetHashCode
			var hash = Value.GetHashCode();
			return hash;
		}

		public int CompareTo(YearMonth other)
		{
			var compareTo = Value.CompareTo(other.Value);
			return compareTo;
		}

		public static bool operator ==(YearMonth c1, YearMonth c2)
		{
			return c1.Equals(c2);
		}

		public static bool operator !=(YearMonth c1, YearMonth c2)
		{
			return !c1.Equals(c2);
		}

		public static bool operator >(YearMonth c1, YearMonth c2)
		{
			return c1.CompareTo(c2) > 0;
		}

		public static bool operator <=(YearMonth c1, YearMonth c2)
		{
			return c1.CompareTo(c2) <= 0;
		}

		public static bool operator <(YearMonth c1, YearMonth c2)
		{
			return c1.CompareTo(c2) < 0;
		}

		public static bool operator >=(YearMonth c1, YearMonth c2)
		{
			return c1.CompareTo(c2) >= 0;
		}

		public static YearMonth operator ++(YearMonth yearMonth)
		{
			var year = yearMonth.Year;
			var month = yearMonth.Month;
			if (month == 12)
			{
				month = 1;
				year++;
			}
			else
			{
				month++;
			}
			return new YearMonth(year, month);
		}

		public static YearMonth operator --(YearMonth yearMonth)
		{
			var year = yearMonth.Year;
			var month = yearMonth.Month;
			if (month == 1)
			{
				month = 12;
				year--;
			}
			else
			{
				month--;
			}
			return new YearMonth(year, month);
		}

		#endregion

		public override string ToString()
		{
			var result = Date().ToString(Constants.IsoYearMonth);
			return result;
		}

		public string ToString(string format)
		{
			var result = Date().ToString(format);
			return result;
		}

		public DateTime Date(bool lastDayOfMonth = false)
		{
			var year = 1;
			var month = 1;

			if (Month != 0 && Year != 0)
			{
				year = Year;
				month = Month;
			}

			var date = new DateTime(year, month, (lastDayOfMonth) ? DateTime.DaysInMonth(year, month) : 1);
			return date;
		}

		/// <summary>
		/// Gets DateRange for whole month
		/// </summary>
		/// <returns></returns>
		public Tuple<DateTime, DateTime> DateRange()
		{
			var startDate = new DateTime(Year, Month, 1);
			var endDate = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month));

			var range = new Tuple<DateTime, DateTime>(startDate, endDate);
			return range;
		}

		public int DiffInMonth(YearMonth obj)
		{
			var firstDate = new DateTime(Year, Month, 1);
			var secondDate = new DateTime(obj.Year, obj.Month, DateTime.DaysInMonth(obj.Year, obj.Month));

			var timeSpan = secondDate - firstDate;
			var result = Convert.ToDouble(timeSpan.Days / 30.0);
			var months = Math.Round(result, MidpointRounding.ToEven);

			return Convert.ToInt32(months);
		}

		public YearMonth AddMonths(int months)
		{
			var dateTime = Date().AddMonths(months);
			return new YearMonth((short)dateTime.Year, (short)dateTime.Month);
		}

		public bool IsGreaterOrEqualThan(YearMonth obj)
		{
			var firstDate = new DateTime(Year, Month, 1);
			var secondDate = new DateTime(obj.Year, obj.Month, 1);

			return firstDate >= secondDate;
		}

		public IReadOnlyCollection<YearMonth> GetMonthsUntil(YearMonth yearMonth)
		{
			var qtdMonth = DiffInMonth(yearMonth);
			var period = new YearMonth(Year, Month);
			var list = new List<YearMonth>();

			for (var i = 1; i <= qtdMonth; i++)
			{
				list.Add(period);
				++period;
			}
			return list;
		}
	}
}

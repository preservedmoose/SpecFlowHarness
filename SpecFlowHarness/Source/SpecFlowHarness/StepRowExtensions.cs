using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace PreservedMoose.SpecFlowHarness
{
	// provide an easier way to call the validation method
	public static class StepRowExtensions
	{
		private const string IsDifferent = nameof(BaseStepRow.IsDifferent);
		private const string SpacingFormatterPattern = "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))";
		private const string SpacingFormatterReplacement = "$1 ";

		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// perform the validation on the specified properties for all rows
		/// </summary>
		/// <typeparam name="TStepRow">the derived class</typeparam>
		/// <param name="rows"></param>
		internal static void Validate<TStepRow>(this IReadOnlyCollection<TStepRow> rows)
			where TStepRow : BaseStepRow
		{
			// perform validation on all rows
			var hasParseErrors = false;

			foreach (var row in rows)
			{
				row.Validate();
				if (row.ParseErrors.Count > 0) hasParseErrors = true;
			}
			if (!hasParseErrors) return;

			// build up the error message information
			var messageBuilder = new StringBuilder();

			messageBuilder.AppendLine(Resources.StepRowExtensions_TableParsingErrorMessage);
			messageBuilder.AppendLine(typeof(TStepRow).ToString());
			messageBuilder.AppendLine();

			foreach (var parseError in rows.SelectMany(row => row.ParseErrors))
			{
				messageBuilder.AppendLine(parseError);
			}
			var message = messageBuilder.ToString();

			// throw an exception and stop
			// ReSharper disable once ConditionIsAlwaysTrueOrFalse
			hasParseErrors.Should().BeFalse(message);
		}

		/// <summary>
		/// Compares this collection of the class to the other.
		/// </summary>
		/// <typeparam name="TStepRow">The type of the step row.</typeparam>
		/// <param name="rowsExpected">The rows expected.</param>
		/// <param name="rowsActual">The rows actual.</param>
		/// <param name="comparisonType">The type of comparison.</param>
		public static void CompareTo<TStepRow>
		(
			this IReadOnlyCollection<TStepRow> rowsExpected,
			IReadOnlyCollection<TStepRow> rowsActual,
			ComparisonType comparisonType = ComparisonType.Equal
		)
			where TStepRow : BaseStepRow, IEquatable<TStepRow>
		{
			Compare(rowsExpected, rowsActual, comparisonType);
		}

		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// take the table of values for this class and convert to the old class
		/// </summary>
		/// <param name="rowsExpected"></param>
		/// <param name="rowsActual"></param>
		/// <param name="isCompareInOrder"></param>
		/// <param name="isContinueOnError"></param>
		/// <returns></returns>
		private static void Compare<TStepRow>
		(
			IReadOnlyCollection<TStepRow> rowsExpected,
			IReadOnlyCollection<TStepRow> rowsActual,
			ComparisonType comparisonType,
			bool isContinueOnError = false
		)
			where TStepRow : BaseStepRow, IEquatable<TStepRow>
		{
			// check the existence of the two collections
			rowsExpected.Should().NotBeNull(Resources.StepRowExtensions_CompareFirstCollectionUndefined);
			rowsActual.Should().NotBeNull(Resources.StepRowExtensions_CompareSecondCollectionUndefined);

			var isDifferent = false;

			// check the sizes of the two collections
			switch (comparisonType)
			{
				case ComparisonType.Equal:
				case ComparisonType.EqualOrdered:
					{
						isDifferent = rowsExpected.Count != rowsActual.Count;
						break;
					}
				case ComparisonType.Subset:
					{
						isDifferent = rowsExpected.Count <= rowsActual.Count;
						break;
					}
				case ComparisonType.Superset:
					{
						isDifferent = rowsExpected.Count >= rowsActual.Count;
						break;
					}
				default:
					{
						throw new StepRowException(Resources.StepRowExtensions_ComparisonTypeUnsupported);
					}
			}

			// check the contents of the two collections
			if (!isDifferent)
			{
				if (comparisonType == ComparisonType.Equal ||
					comparisonType == ComparisonType.Subset ||
					comparisonType == ComparisonType.Superset)
				{
					if (comparisonType == ComparisonType.Equal ||
						comparisonType == ComparisonType.Superset)
					{
						// determine if the expected list has the elements in the actual
						foreach (var actual in rowsActual)
						{
							if (rowsExpected.Contains(actual)) continue;
							actual.IsDifferent = true;
							isDifferent = true;
						}
					}

					if (comparisonType == ComparisonType.Equal ||
						comparisonType == ComparisonType.Subset)
					{
						// determine if the actual list has the elements in the expected
						foreach (var expected in rowsExpected)
						{
							if (rowsActual.Contains(expected)) continue;
							expected.IsDifferent = true;
							isDifferent = true;
						}
					}
				}
				else if (comparisonType == ComparisonType.EqualOrdered)
				{
					var listActual = rowsActual.ToList();
					var listExpected = rowsExpected.ToList();

					// determine if the expected list has the elements in the actual
					for (var index = 0; index < listActual.Count; ++index)
					{
						var actual = listActual[index];
						var expected = listExpected[index];

						if (expected.Equals(actual)) continue;
						actual.IsDifferent = true;
						isDifferent = true;
					}

					// determine if the actual list has the elements in the expected
					for (var index = 0; index < listExpected.Count; ++index)
					{
						var actual = listActual[index];
						var expected = listExpected[index];

						if (actual.Equals(expected)) continue;
						expected.IsDifferent = true;
						isDifferent = true;
					}
				}
			}

			// return if we have a match
			if (isDifferent)
			{
				DisplayErrors(rowsExpected, rowsActual, isContinueOnError);
			}
		}

		private static void DisplayErrors<TStepRow>(IReadOnlyCollection<TStepRow> rowsExpected, IReadOnlyCollection<TStepRow> rowsActual, bool isContinueOnError)
			where TStepRow : BaseStepRow
		{
			// we need to display the errors to the user on the console window
			var usedColumns = rowsExpected.SelectMany(r => r.UsedProperties).Distinct().ToList();

			var displayActual = rowsActual.ToDataTable(usedColumns).ToDisplayString();
			var displayExpected = rowsExpected.ToDataTable(usedColumns).ToDisplayString();

			var messageBuilder = new StringBuilder();

			messageBuilder.AppendLine(Resources.StepRowExtensions_CompareCollectionsAreDifferent);

			if (isContinueOnError)
			{
				messageBuilder.AppendLine(Resources.StepRowExtensions_CompareContinueOnErrorWarning);
			}
			messageBuilder.AppendLine();
			messageBuilder.AppendLine(Resources.StepRowExtensions_CompareExpectedResults);
			messageBuilder.AppendLine(displayExpected);
			messageBuilder.AppendLine(Resources.StepRowExtensions_CompareActualResults);
			messageBuilder.AppendLine(displayActual);

			var message = messageBuilder.ToString();

			if (!isContinueOnError)
			{
				// throw an exception and stop
				// ReSharper disable once ConditionIsAlwaysTrueOrFalse
				true.Should().BeFalse(message);
			}

			// just display the error and continue
			Console.WriteLine(message);
		}

		/// <summary>
		/// convert to DataTable so that we can return more information
		/// Enums are converted to strings with spacing between the words
		/// </summary>
		/// <param name="rows"></param>
		/// <param name="usedColumns"></param>
		/// <returns></returns>
		private static DataTable ToDataTable<TStepRow>(this IReadOnlyCollection<TStepRow> rows, IReadOnlyCollection<string> usedColumns)
			where TStepRow : BaseStepRow
		{
			var type = typeof(TStepRow);
			var baseType = typeof(StepRow<TStepRow>);

			var baseProperties = baseType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			var derivedProperties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			var isDifferentProperty = baseProperties.Single(p => p.Name == IsDifferent);
			var basePropertyNames = baseProperties.Select(p => p.Name);
			var properties = derivedProperties.Where(p => !basePropertyNames.Contains(p.Name)).OrderBy(p => p.Name).ToList();

			var dataTable = new DataTable();

			// Create the columns in the DataTable
			dataTable.Columns.Add("_", typeof(char));   // IsDifferent

			foreach (var propertyInfo in properties)
			{
				if (usedColumns.Count > 0 && !usedColumns.Contains(propertyInfo.Name)) continue;

				var isCollection = propertyInfo.PropertyType.IsConstructedGenericType &&
									typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType);
				var isConvertedToString = propertyInfo.PropertyType.IsEnum || propertyInfo.PropertyType == typeof(DateTime);

				dataTable.Columns.Add(propertyInfo.Name, (isCollection || isConvertedToString) ? typeof(string) : propertyInfo.PropertyType);
			}

			// Populate the table
			foreach (var row in rows)
			{
				var dataRow = dataTable.NewRow();
				var isDifferent = (bool)isDifferentProperty.GetValue(row, null);

				dataRow.BeginEdit();
				dataRow["_"] = isDifferent ? '*' : ' ';

				foreach (var propertyInfo in properties)
				{
					if (usedColumns.Count > 0 && !usedColumns.Contains(propertyInfo.Name)) continue;

					var value = propertyInfo.GetValue(row, null);
					var isCollection = propertyInfo.PropertyType.IsConstructedGenericType &&
										typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType);

					var propertyType = (isCollection) ? propertyInfo.PropertyType.GetGenericArguments()[0] : propertyInfo.PropertyType;

					if (!isCollection)
					{
						var data = FormatData(propertyType, value);
						dataRow[propertyInfo.Name] = data;
					}
					else
					{
						if (value != null)
						{
							dynamic values = value;
							var formatted = new Collection<string>();

							foreach (var item in values)
							{
								var data = FormatData(propertyType, item);
								formatted.Add(data.ToString());
							}
							dataRow[propertyInfo.Name] = string.Join(Constants.CommaSeparator, formatted.OrderBy(f => f));
						}
					}
				}

				dataRow.EndEdit();
				dataTable.Rows.Add(dataRow);
			}

			// use a RegEx to add spaces between the words for the columns names
			foreach (DataColumn column in dataTable.Columns)
			{
				column.ColumnName = Regex.Replace(column.ColumnName, SpacingFormatterPattern, SpacingFormatterReplacement);
			}
			return dataTable;
		}

		private static dynamic FormatData(Type type, object value)
		{
			dynamic data;

			if (type.IsEnum)
			{
				// convert to a string and then use a RegEx to add spaces between the words
				data = Regex.Replace(value.ToString(), SpacingFormatterPattern, SpacingFormatterReplacement);
			}
			else if (type == typeof(DateTime))
			{
				// convert to a string to display in ISO format
				data = ((DateTime)value).ToString(Constants.IsoDateTime);
			}
			else
			{
				data = value;
			}
			return data;
		}
	}
}

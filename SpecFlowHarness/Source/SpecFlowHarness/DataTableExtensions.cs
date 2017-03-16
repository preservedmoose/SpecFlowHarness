using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CIAndT.SpecFlowHarness
{
	/// <summary>
	/// Helper extension methods for handling DataTable modifications
	/// </summary>
	public static class DataTableExtensions
	{
		private const string Separator = " | ";

		/// <summary>
		/// returns the table as a string representation
		/// </summary>
		/// <param name="dataTable"></param>
		/// <returns></returns>
		public static string ToDisplayString(this DataTable dataTable)
		{
			var stringBuilder = new StringBuilder();
			var dictionary = new Dictionary<string, int>();

			// set the structure to hold the columns and the initial widths of these
			foreach (DataColumn column in dataTable.Columns)
			{
				dictionary[column.ColumnName] = column.ColumnName.Length;
			}

			// look through all of the rows for any that are wider than the headers
			foreach (DataRow row in dataTable.Rows)
			{
				for (var iColumn = 0; iColumn < row.ItemArray.Length; iColumn++)
				{
					if (row[iColumn] == null) continue;

					var iWidth = (row[iColumn] is DateTime) ? Constants.IsoDateTime.Length : row[iColumn].ToString().Length;

					// if this row is wider then widen our column size
					if (iWidth > dictionary[dataTable.Columns[iColumn].ColumnName])
					{
						dictionary[dataTable.Columns[iColumn].ColumnName] = iWidth;
					}
				}
			}

			var rowLength = (dictionary.Values.Count + 1) * Separator.Length;
			foreach (var value in dictionary.Values) rowLength += value;

			// separator bar
			stringBuilder.AppendLine(new string('-', rowLength));

			// put in the headers
			foreach (DataColumn column in dataTable.Columns)
			{
				var format = "{0,-" + dictionary[column.ColumnName] + "}";
				stringBuilder.Append(Separator + string.Format(format, column.ColumnName));
			}
			stringBuilder.AppendLine(Separator);

			// separator bar
			stringBuilder.AppendLine(new string('-', rowLength));

			// put in the content
			foreach (DataRow row in dataTable.Rows)
			{
				for (var iColumn = 0; iColumn < row.ItemArray.Length; iColumn++)
				{
					var iColumnWidth = dictionary[dataTable.Columns[iColumn].ColumnName];
					stringBuilder.Append(Separator);

					var rightAlign = row[iColumn] is ValueType;
					var format = "{0," + (rightAlign ? ' ' : '-') + iColumnWidth + "}";
					stringBuilder.AppendFormat(format, row[iColumn]);

					if (iColumn == row.ItemArray.Length - 1) stringBuilder.AppendLine(Separator);
				}
			}

			// separator bar
			stringBuilder.AppendLine(new string('-', rowLength));

			return stringBuilder.ToString();
		}
	}
}

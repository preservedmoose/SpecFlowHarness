using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PreservedMoose.SpecFlowHarness.FunctionalTests.StepRows
{
	public sealed class LargeTestResult
	{
		public string Quantity1 { get; set; }
		public string Quantity2 { get; set; }
		public string Quantity3 { get; set; }
		public string Quantity4 { get; set; }
		public string Quantity5 { get; set; }
		public string Quantity6 { get; set; }
		public string PurchaseDate { get; set; }

		public static IReadOnlyCollection<LargeTestResult> GetTestData()
		{
			var purchaseDate = new DateTime(2017, 1, 1);

			var rows = new Collection<LargeTestResult>();

			for (var index = 0; index < 60; ++index)
			{
				rows.Add(new LargeTestResult
				{
					Quantity1 = (1000 + index).ToString("n0"),
					Quantity2 = (2000 + index).ToString("n0"),
					Quantity3 = (3000 + index).ToString("n0"),
					Quantity4 = (4000 + index).ToString("n0"),
					Quantity5 = (5000 + index).ToString("n0"),
					Quantity6 = (6000 + index).ToString("n0"),
					PurchaseDate = purchaseDate.ToString(Constants.IsoDate)
				});

				purchaseDate = purchaseDate.AddDays(1);
			}
			return rows;
		}
	}
}

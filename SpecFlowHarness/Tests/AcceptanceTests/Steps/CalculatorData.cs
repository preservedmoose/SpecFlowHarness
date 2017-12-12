using PreservedMoose.SpecFlowHarness.AcceptanceTests.StepRows;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PreservedMoose.SpecFlowHarness.AcceptanceTests.Steps
{
	public class CalculatorData
	{
		public StepCalculatorRow.Function Function { get; set; }
		public IReadOnlyCollection<StepCalculatorRow> Rows { get; set; }
		public IReadOnlyCollection<StepCalculatorRow> CalculatedRows { get; set; }

		public CalculatorData()
		{
			Rows = new Collection<StepCalculatorRow>();
			CalculatedRows = new Collection<StepCalculatorRow>();
		}
	}
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FluentAssertions;
using PreservedMoose.SpecFlowHarness.AcceptanceTests.StepRows;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace PreservedMoose.SpecFlowHarness.AcceptanceTests.Steps
{
	[Binding]
	[Scope(Tag = "Core")]
	public class CoreThenSteps : CoreSteps
	{
		//--------------------------------------------------------------------------

		public CoreThenSteps(CalculatorData calculatorData)
			: base(calculatorData)
		{
		}

		//--------------------------------------------------------------------------
		// ReSharper disable UnusedMember.Global

		#region Old

		[Then(@"we have \(old\) test results:")]
		public void Then_WeHaveOldTestResults(Table table)
		{
			SetCurrentCulture();

			// compare with data from the system
			var actualRows = TestResult.GetTestData();

			table.CompareToSet(actualRows);
		}

		[Then(@"we have \(old\) test using \(new\) default results:")]
		public void Then_WeHaveOldTestUsingNewDefaultResults(Table table)
		{
			SetCurrentCulture();

			// compare with data from the system
			var actualRows = new Collection<StepTestResultRow> { new StepTestResultRow() };

			var expectedRows = table.CreateSet<StepTestResultRow>().ToList();

			for (var index = 0; index < actualRows.Count; ++index)
			{
				var actualRow = actualRows[index];
				var expectedRow = expectedRows[index];

				actualRow.ShouldBeEquivalentTo(expectedRow);
			}
		}

		[Then(@"we have \(old\) test using \(new\) results:")]
		public void Then_WeHaveOldTestUsingNewResults(Table table)
		{
			SetCurrentCulture();

			// compare with data from the system
			var testData = TestResult.GetTestData();
			var actualRows = StepTestResultRow.Convert(testData);

			var expectedRows = table.CreateSet<StepTestResultRow>().ToList();

			// compare our two sets
			expectedRows.CompareTo(actualRows);
			expectedRows.OrderedCompareTo(actualRows);
		}

		[Then(@"we have \(old\) large test results:")]
		public void Then_WeHaveOldLargeTestResults(Table table)
		{
			SetCurrentCulture();

			// compare with data from the system
			var actualRows = LargeTestResult.GetTestData();
			// does not convert the integers!

			// ReSharper disable once UnusedVariable
			var expectedRows = table.CreateSet<StepLargeTestResultRow>().ToList();

			table.CompareToSet(actualRows);
		}

		#endregion

		//--------------------------------------------------------------------------

		#region New

		[Then(@"we have \(new\) test default results:")]
		public void Then_WeHaveNewTestDefaultResults(IReadOnlyCollection<StepTestResultRow> expectedRows)
		{
			SetCurrentCulture();

			// compare with data from the system
			var actualRows = new Collection<StepTestResultRow> { new StepTestResultRow() };

			// call the comparator to compare our two sets
			expectedRows.CompareTo(actualRows);
			expectedRows.OrderedCompareTo(actualRows);
		}

		[Then(@"we have \(new\) test results:")]
		public void Then_WeHaveNewTestResults(IReadOnlyCollection<StepTestResultRow> expectedRows)
		{
			SetCurrentCulture();

			// compare with data from the system
			var testData = TestResult.GetTestData();
			var actualRows = StepTestResultRow.Convert(testData);

			// call the comparator to compare our two sets
			expectedRows.CompareTo(actualRows);
			expectedRows.OrderedCompareTo(actualRows);
		}

		[Then(@"we have \(new\) large test results:")]
		public void Then_WeHaveNewLargeTestResults(IReadOnlyCollection<StepLargeTestResultRow> expectedRows)
		{
			SetCurrentCulture();

			// compare with data from the system
			var testData = LargeTestResult.GetTestData();
			var actualRows = StepLargeTestResultRow.Convert(testData);

			// call the comparator to compare our two sets
			expectedRows.CompareTo(actualRows);
			expectedRows.OrderedCompareTo(actualRows);
		}

		#endregion

		[Then(@"we have no errors!")]
		public void Then_WeHaveNoErrors()
		{
			SetCurrentCulture();
		}

		[Then(@"we have these values:")]
		public void ThenWeHaveTheseValues(IReadOnlyCollection<StepCalculatorRow> expectedRows)
		{
			SetCurrentCulture();

			var actualRows = Data.CalculatedRows;
			actualRows.Should().NotBeNull();

			expectedRows.CompareTo(actualRows);
		}

		// ReSharper restore UnusedMember.Global
		//--------------------------------------------------------------------------
	}
}

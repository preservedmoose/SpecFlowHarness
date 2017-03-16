using System;
using System.Collections.Generic;
using System.Linq;
using CIAndT.SpecFlowHarness.FunctionalTests.StepRows;
using CIAndT.SpecFlowHarness.FunctionalTests.TestClasses;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CIAndT.SpecFlowHarness.FunctionalTests.Steps
{
	[Binding]
	[Scope(Tag = "Core")]
	public class CoreWhenSteps : CoreSteps
	{
		//--------------------------------------------------------------------------

		public CoreWhenSteps(CalculatorData calculatorData)
			: base(calculatorData)
		{
		}

		//--------------------------------------------------------------------------
		// ReSharper disable UnusedMember.Global

		#region Old

		[When(@"we have \(old\) these strings: (.+)")]
		public void When_WeHaveOldTheseStrings(string commaSeparatedValues)
		{
			SetCurrentCulture();

			var values = commaSeparatedValues.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim()).ToList();

			values.Should().NotBeNull();
		}

		[When(@"we have \(old\) these integers: (.+)")]
		public void When_WeHaveOldTheseIntegers(string commaSeparatedValues)
		{
			SetCurrentCulture();

			var values = commaSeparatedValues.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Select(v => int.Parse(v.Trim())).ToList();

			values.Should().NotBeNull();
		}

		[When(@"we have \(old\) these colours: (.+)")]
		public void When_WeHaveOldTheseColours(string commaSeparatedValues)
		{
			SetCurrentCulture();

			var stringValues = commaSeparatedValues.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim()).ToList();
			var values = stringValues.Select(s => Enum.Parse(typeof(Colour), s)).ToList();

			values.Should().NotBeNull();
		}

		[When(@"we perform \(old\) some background step with this data:")]
		public void When_WePerformOldSomeBackgroundStepWithThisData(Table table)
		{
			SetCurrentCulture();

			var expectedRows = table.CreateSet<StepTestRow>();

			expectedRows.Should().NotBeNull();
		}

		[When(@"we test \(old\) the nullable step row class:")]
		public void When_WeTestOldTheNullableStepRowClass(Table table)
		{
			SetCurrentCulture();

			var expectedRows = table.CreateSet<StepTestNullableRow>();

			expectedRows.Should().NotBeNull();
		}

		[When(@"we test \(old\) the step row class:")]
		public void When_WeTestOldTheStepRowClass(Table table)
		{
			SetCurrentCulture();

			var expectedRows = table.CreateSet<StepTestRow>();

			expectedRows.Should().NotBeNull();
		}

		#endregion

		//--------------------------------------------------------------------------

		#region New

		[When(@"we have \(new\) these strings: (.+)")]
		public void When_WeHaveNewTheseStrings(IReadOnlyCollection<string> values)
		{
			SetCurrentCulture();

			values.Should().NotBeNull();
		}

		[When(@"we have \(new\) these integers: (.+)")]
		public void When_WeHaveNewTheseIntegers(IReadOnlyCollection<int> values)
		{
			SetCurrentCulture();

			values.Should().NotBeNull();
		}

		[When(@"we have \(new\) these colours: (.+)")]
		public void When_WeHaveNewTheseColours(IReadOnlyCollection<Colour> values)
		{
			SetCurrentCulture();

			// look in the Transforms file...
			values.Should().NotBeNull();
		}

		[When(@"we perform \(new\) some background step with this data:")]
		public void When_WePerformNewSomeBackgroundStepWithThisData(IReadOnlyCollection<StepTestRow> expectedRows)
		{
			SetCurrentCulture();
		}

		[When(@"we test \(new\) the nullable step row class:")]
		public void When_WeTestNewTheNullableStepRowClass(IReadOnlyCollection<StepTestNullableRow> expectedRows)
		{
			SetCurrentCulture();

			expectedRows.Should().NotBeNull();
		}

		[When(@"we test \(new\) the step row class:")]
		public void When_WeTestNewTheStepRowClass(IReadOnlyCollection<StepTestRow> expectedRows)
		{
			SetCurrentCulture();

			expectedRows.Should().NotBeNull();
		}

		#endregion

		//--------------------------------------------------------------------------

		[When(@"we set the time to (.+?)")]
		public void When_WeSetTheTimeTo(DateTime dateTime)
		{
			SetCurrentCulture();

			dateTime.Should().NotBe(DateTime.MinValue, "The time was not set!");
		}

		[When(@"we test (.+?) and (.+?) for time (.+?)")]
		public void When_WeTestCycleAndYearMonthAndTime(CycleType cycleType, YearMonth yearMonth, DateTime dateTime)
		{
			SetCurrentCulture();

			cycleType.Should().NotBeNull("The Cycle Type was not found!");
			yearMonth.Should().NotBeNull("The Year Month was not found!");
			dateTime.Should().NotBe(DateTime.MinValue, "The DateTime was not set!");
		}

		[When(@"we do nothing!")]
		public void When_WeDoNothing()
		{
			SetCurrentCulture();
		}

		[When(@"we use the calculator (.+?) function")]
		public void When_WeUseTheCalculatorLogFunction(StepCalculatorRow.Function function)
		{
			SetCurrentCulture();

			//function.Should().NotBe(StepCalculatorRow.Function.None);

			Data.CalculatedRows = StepCalculatorRow.Calculate(Data.Rows, function);
		}

		// ReSharper restore UnusedMember.Global
		//--------------------------------------------------------------------------
	}
}

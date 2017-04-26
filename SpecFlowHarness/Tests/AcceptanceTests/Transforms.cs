using System;
using System.Collections.Generic;
using PreservedMoose.SpecFlowHarness.AcceptanceTests.StepRows;
using PreservedMoose.SpecFlowHarness.TestClasses;
using TechTalk.SpecFlow;
using TinyIoC;

namespace PreservedMoose.SpecFlowHarness.AcceptanceTests
{
	[Binding]
	public class Transforms : BaseTransforms
	{
		private readonly TinyIoCContainer _container;

		public Transforms(TinyIoCContainer container)
		{
			_container = container;
			container.Register<IConverter, Converter>();
		}

		// ---------------------------------------------------------------------------------------

		/// <summary>
		/// explicit conversion of string to a Colour enum
		/// </summary>
		/// <param name="value">string to convert</param>
		/// <returns>enum value represented</returns>
		[StepArgumentTransformation(@"(.+) colour")]
		[StepArgumentTransformation(@"colour (.+)")]
		public Colour TransformColour(string value)
		{
			var converter = _container.Resolve<IConverter>();

			var colour = converter.ToEnum<Colour>(value);
			return colour;
		}

		/// <summary>
		/// implicit conversion of string to a Colour enum
		/// </summary>
		/// <param name="value">string to convert</param>
		/// <returns>enum value represented</returns>
		[StepArgumentTransformation(@"(.+)")]
		[StepArgumentTransformation(@"(.+)")]
		public Colour TransformAnyColour(string value)
		{
			var converter = _container.Resolve<IConverter>();

			var colour = converter.ToEnum<Colour>(value);
			return colour;
		}

		/// <summary>
		/// implicit conversion of string to a Colour enum
		/// </summary>
		/// <param name="value">string to convert</param>
		/// <returns>enum value represented</returns>
		[StepArgumentTransformation(@"(.+)")]
		[StepArgumentTransformation(@"(.+)")]
		public IReadOnlyCollection<Colour> TransformAnyColours(string value)
		{
			var converter = _container.Resolve<IConverter>();

			var colours = converter.ToEnums<Colour>(value);
			return colours;
		}

		// ---------------------------------------------------------------------------------------

		/// <summary>
		/// explicit transformation to a YearMonth object
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		[StepArgumentTransformation(@"(.+) period")]
		[StepArgumentTransformation(@"period (.+)")]
		public YearMonth TransformYearMonth(string value)
		{
			var converter = _container.Resolve<IConverter>();

			var yearMonth = converter.ToObject<YearMonth>(value);
			return yearMonth;
		}

		/// <summary>
		/// implicit transformation to a YearMonth object
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		[StepArgumentTransformation(@"(.+)")]
		public YearMonth TransformAnyYearMonth(string value)
		{
			var converter = _container.Resolve<IConverter>();

			var yearMonth = converter.ToObject<YearMonth>(value);
			return yearMonth;
		}

		// ---------------------------------------------------------------------------------------

		/// <summary>
		/// explicit transformation to a cycle object
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		[StepArgumentTransformation(@"(.+) cycle")]
		public CycleType TransformCycleType(string value)
		{
			var converter = _container.Resolve<IConverter>();

			var cycleType = converter.ToObjectStatic<CycleType>(value);
			return cycleType;
		}

		// ---------------------------------------------------------------------------------------

		/// <summary>
		/// explicit transformation to a cycle object
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		[StepArgumentTransformation(@"(.+) Drn")]
		public int TransformDrn(string value)
		{
			var converter = _container.Resolve<IConverter>();

			var drn = converter.ToValue<int>(value);
			return drn;
		}

		// ---------------------------------------------------------------------------------------

		/// <summary>
		/// explicit transformation to a DateTime day
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		[StepArgumentTransformation(@"(.+) day")]
		[StepArgumentTransformation(@"day (.+)")]
		public DateTime TransformDay(string value)
		{
			var converter = _container.Resolve<IConverter>();

			var day = converter.ToValue<DateTime>(value);
			return day;
		}

		// ---------------------------------------------------------------------------------------

		/// <summary>
		/// explicit transformation to a Function
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		[StepArgumentTransformation(@"(.+)")]
		public StepCalculatorRow.Function TransformAnyFunction(string value)
		{
			var converter = _container.Resolve<IConverter>();

			var function = converter.ToEnum<StepCalculatorRow.Function>(value);
			return function;
		}

		// ---------------------------------------------------------------------------------------
	}
}

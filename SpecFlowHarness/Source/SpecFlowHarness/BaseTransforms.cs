using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace CIAndT.SpecFlowHarness
{
	[Binding]
	public class BaseTransforms
	{
		private readonly IConverter _converter;

		public BaseTransforms()
		{
			_converter = new Converter();
		}

		// ---------------------------------------------------------------------------------------

		/// <summary>
		/// implicit transformation
		/// </summary>
		/// <param name="fromValue">value</param>
		/// <returns>value converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public Boolean TransformTo_Boolean(String fromValue)
		{
			var toValue = _converter.ToValue<Boolean>(fromValue);
			return toValue;
		}

		/// <summary>
		/// implicit transformation
		/// </summary>
		/// <param name="fromValue">value</param>
		/// <returns>value converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public Int16 TransformTo_Int16(String fromValue)
		{
			var toValue = _converter.ToValue<Int16>(fromValue);
			return toValue;
		}

		/// <summary>
		/// implicit transformation
		/// </summary>
		/// <param name="fromValue">value</param>
		/// <returns>value converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public Int32 TransformTo_Int32(String fromValue)
		{
			var toValue = _converter.ToValue<Int32>(fromValue);
			return toValue;
		}

		/// <summary>
		/// implicit transformation
		/// </summary>
		/// <param name="fromValue">value</param>
		/// <returns>value converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public Int64 TransformTo_Int64(String fromValue)
		{
			var toValue = _converter.ToValue<Int64>(fromValue);
			return toValue;
		}

		/// <summary>
		/// implicit transformation
		/// </summary>
		/// <param name="fromValue">value</param>
		/// <returns>value converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public Single TransformTo_Single(String fromValue)
		{
			var toValue = _converter.ToValue<Single>(fromValue);
			return toValue;
		}

		/// <summary>
		/// implicit transformation
		/// </summary>
		/// <param name="fromValue">value</param>
		/// <returns>value converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public Double TransformTo_Double(String fromValue)
		{
			var toValue = _converter.ToValue<Double>(fromValue);
			return toValue;
		}

		/// <summary>
		/// implicit transformation
		/// </summary>
		/// <param name="fromValue">value</param>
		/// <returns>value converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public Decimal TransformTo_Decimal(String fromValue)
		{
			var toValue = _converter.ToValue<Decimal>(fromValue);
			return toValue;
		}

		/// <summary>
		/// implicit transformation
		/// </summary>
		/// <param name="fromValue">value</param>
		/// <returns>value converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public DateTime TransformTo_DateTime(String fromValue)
		{
			var toValue = _converter.ToValue<DateTime>(fromValue);
			return toValue;
		}

		// ---------------------------------------------------------------------------------------

		/// <summary>
		/// implicit transformation to collection
		/// </summary>
		/// <param name="fromValues">comma separated values</param>
		/// <returns>collection of values converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public IReadOnlyCollection<Boolean> TransformToCollection_Boolean(String fromValues)
		{
			var toValues = _converter.ToValues<Boolean>(fromValues);
			return toValues;
		}

		/// <summary>
		/// implicit transformation to collection
		/// </summary>
		/// <param name="fromValues">comma separated values</param>
		/// <returns>collection of values converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public IReadOnlyCollection<Int16> TransformToCollection_Int16(String fromValues)
		{
			var toValues = _converter.ToValues<Int16>(fromValues);
			return toValues;
		}

		/// <summary>
		/// implicit transformation to collection
		/// </summary>
		/// <param name="fromValues">comma separated values</param>
		/// <returns>collection of values converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public IReadOnlyCollection<Int32> TransformToCollection_Int32(String fromValues)
		{
			var toValues = _converter.ToValues<Int32>(fromValues);
			return toValues;
		}

		/// <summary>
		/// implicit transformation to collection
		/// </summary>
		/// <param name="fromValues">comma separated values</param>
		/// <returns>collection of values converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public IReadOnlyCollection<Int64> TransformToCollection_Int64(String fromValues)
		{
			var toValues = _converter.ToValues<Int64>(fromValues);
			return toValues;
		}

		/// <summary>
		/// implicit transformation to collection
		/// </summary>
		/// <param name="fromValues">comma separated values</param>
		/// <returns>collection of values converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public IReadOnlyCollection<Single> TransformToCollection_Single(String fromValues)
		{
			var toValues = _converter.ToValues<Single>(fromValues);
			return toValues;
		}

		/// <summary>
		/// implicit transformation to collection
		/// </summary>
		/// <param name="fromValues">comma separated values</param>
		/// <returns>collection of values converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public IReadOnlyCollection<Double> TransformToCollection_Double(String fromValues)
		{
			var toValues = _converter.ToValues<Double>(fromValues);
			return toValues;
		}

		/// <summary>
		/// implicit transformation to collection
		/// </summary>
		/// <param name="fromValues">comma separated values</param>
		/// <returns>collection of values converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public IReadOnlyCollection<Decimal> TransformToCollection_Decimal(String fromValues)
		{
			var toValues = _converter.ToValues<Decimal>(fromValues);
			return toValues;
		}

		/// <summary>
		/// implicit transformation to collection
		/// </summary>
		/// <param name="fromValues">comma separated values</param>
		/// <returns>collection of values converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public IReadOnlyCollection<DateTime> TransformToCollection_DateTime(String fromValues)
		{
			var toValues = _converter.ToValues<DateTime>(fromValues);
			return toValues;
		}

		/// <summary>
		/// implicit transformation to collection
		/// </summary>
		/// <param name="fromValues">comma separated values</param>
		/// <returns>collection of values converted to type</returns>
		[StepArgumentTransformation(@"(.+)")]
		public IReadOnlyCollection<String> TransformToCollection_String(String fromValues)
		{
			var toValues = _converter.ToValues<String>(fromValues);
			return toValues;
		}

		// ---------------------------------------------------------------------------------------
	}
}

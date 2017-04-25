using System.Collections.Generic;

namespace PreservedMoose.SpecFlowHarness.UnitTests.TestClasses
{
	public class CycleType : IConvertibleStaticFromString<CycleType>
	{
		public int Id { get; }

		public string Description { get; }

		/// <summary>
		/// Static instance of Timely cycle
		/// </summary>
		public static readonly CycleType Timely = new CycleType(1, "Timely");
		/// <summary>
		/// Static instance of Evening cycle
		/// </summary>
		public static readonly CycleType Evening = new CycleType(4, "Evening");

		// caching
		private static readonly Dictionary<int, CycleType> IdMap = new Dictionary<int, CycleType>
		{
			{Timely.Id, Timely},
			{Evening.Id, Evening}
		};

		private static readonly Dictionary<string, CycleType> NameMap = new Dictionary<string, CycleType>
		{
			{Timely.Description, Timely},
			{Evening.Description, Evening}
		};

		/// <summary>
		/// Creates an instance of the CycleType class
		/// </summary>
		public CycleType()
		{
		}

		/// <summary>
		/// Creates an instance of the CycleType class
		/// </summary>
		/// <param name="id">CycleType id</param>
		/// <param name="description">CycleType description</param>
		public CycleType(int id, string description)
		{
			Id = id;
			Description = description;
		}

		/// <summary>
		/// Gets the CycleType by its Id
		/// </summary>
		/// <param name="id">CycleType Id</param>
		/// <returns>CycleType object</returns>
		public static CycleType Get(int id)
		{
			return IdMap[id];
		}

		/// <summary>
		/// Gets the CycleType by its description
		/// </summary>
		/// <param name="description">CycleType description</param>
		/// <returns>CycleType object</returns>
		public static CycleType Get(string description)
		{
			var cycleType = NameMap[description];
			return cycleType;
		}

		/// <summary>
		/// Returns a formated description for the CycleType
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			var toString = $"id: {Id}, description: {Description}";
			return toString;
		}

		/// <summary>
		/// Try to parse a string value into a CycleType result
		/// </summary>
		/// <param name="value">string value to parse</param>
		/// <param name="result">Out parameter with the parse result</param>
		/// <returns>Returns True case the parse is successful, otherwise, returns False</returns>
		public bool TryParse(string value, out CycleType result)
		{
			var wasParsed = NameMap.TryGetValue(value, out result);
			return wasParsed;
		}
	}
}

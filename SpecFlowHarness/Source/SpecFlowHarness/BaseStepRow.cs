using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using TechTalk.SpecFlow;

namespace PreservedMoose.SpecFlowHarness
{
	public abstract class BaseStepRow
	{
		protected readonly IParser Parser;

		// we need to call using reflection
		// so that we can pass the type as a generic to the method
		private readonly MethodInfo _methodInfoEnum;
		private readonly MethodInfo _methodInfoObject;
		private readonly MethodInfo _methodInfoObjectStatic;

		private const string ParseEnum = nameof(IParser.ParseEnum);
		private const string ParseObject = nameof(IParser.ParseObject);
		private const string ParseObjectStatic = nameof(IParser.ParseObjectStatic);

		protected static readonly Type[] SimpleTypes =
		{
				 typeof(bool),
				 typeof(byte),
				 typeof(char),
				 typeof(short),
				 typeof(int),
				 typeof(long),
				 typeof(float),
				 typeof(double),
				 typeof(decimal),
				 typeof(sbyte),
				 typeof(ushort),
				 typeof(uint),
				 typeof(ulong),
				 typeof(string),
				 typeof(DateTime)
		};

		// comparison flag indicating that the object is different (not found in the other table)
		public bool IsDifferent { set; get; }

		// collection for any errors found in the object when attempting to parse the table
		public IList<string> ParseErrors { get; }

		// collection for all columns found populated with data when parsing the table
		public IList<string> UsedProperties { get; }

		// ----------------------------------------------------------------------------------------

		protected BaseStepRow()
		{
			Parser = new Parser();

			ParseErrors = new List<string>();
			UsedProperties = new List<string>();

			// we may need to handle Objects using reflection to get the correct type
			var type = Parser.GetType();

			_methodInfoEnum = type.GetMethod(ParseEnum, BindingFlags.Instance | BindingFlags.Public);
			_methodInfoObject = type.GetMethod(ParseObject, BindingFlags.Instance | BindingFlags.Public);
			_methodInfoObjectStatic = type.GetMethod(ParseObjectStatic, BindingFlags.Instance | BindingFlags.Public);

			_methodInfoEnum.Should().NotBeNull(Resources.BaseStepRow_MethodInfo, ParseEnum);
			_methodInfoObject.Should().NotBeNull(Resources.BaseStepRow_MethodInfo, ParseObject);
			_methodInfoObjectStatic.Should().NotBeNull(Resources.BaseStepRow_MethodInfo, ParseObjectStatic);
		}

		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// perform validation requested by the overridden class for the Validation method
		/// </summary>
		public void Validate()
		{
			Validation();
		}

		/// <summary>
		/// specify any validation that we wish to perform when we call the Validate method
		/// </summary>
		protected virtual void Validation()
		{
		}

		// ----------------------------------------------------------------------------------------


		/// <summary>
		/// ensures that the table does not contains columns that do not map to the class
		/// </summary>
		/// <typeparam name="TStepRow">the class to which we wish to map</typeparam>
		/// <param name="table">the representation of the table in the feature file</param>
		// ReSharper disable once MemberCanBeMadeStatic.Local
		protected void CheckForMissingColumnsOnClass<TStepRow>(Table table)
		{
			var type = typeof(TStepRow);
			var typeInfo = type.GetTypeInfo();
			var listMissingColumns = new List<string>();

			foreach (var sColumn in table.Header)
			{
				var sNarrowColumn = sColumn.Replace(Constants.Space, string.Empty);
				var propertyInfo = typeInfo.GetProperty(sNarrowColumn, BindingFlags.Public | BindingFlags.Instance);
				if (null == propertyInfo) listMissingColumns.Add(sColumn);
			}

			if (0 == listMissingColumns.Count) return;

			// we have errors...
			var errorMessage = string.Format(Resources.BaseStepRow_MissingColumnsCheck, string.Join(Constants.CommaSeparator, listMissingColumns), type);

			// the assertion will throw an exception if this is invalid
			listMissingColumns.Count.Should().Be(0, errorMessage);
		}

		/// <summary>
		/// ensures that the class does not contain nullable types
		/// </summary>
		/// <typeparam name="TStepRow">the class to which we wish to map</typeparam>
		// ReSharper disable once MemberCanBeMadeStatic.Local
		protected void CheckForNullableTypesOnClass<TStepRow>()
			where TStepRow : BaseStepRow
		{
			var type = typeof(TStepRow);
			var baseType = typeof(StepRow<TStepRow>);
			var typeInfo = type.GetTypeInfo();

			var listNullableTypes = new List<string>();

			var baseProperties = baseType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			var derivedProperties = typeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Public);

			var basePropertyNames = baseProperties.Select(p => p.Name);
			var properties = derivedProperties.Where(p => !basePropertyNames.Contains(p.Name)).ToList();

			// we can catch nullable types here for value types and enums
			foreach (var propertyInfo in properties)
			{
				var isCollection = propertyInfo.PropertyType.IsConstructedGenericType &&
									typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType);

				if (!isCollection)
				{
					if (propertyInfo.PropertyType.IsGenericType &&
						propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
					{
						listNullableTypes.Add(propertyInfo.Name);
					}
				}
				else
				{
					var propertyType = propertyInfo.PropertyType.GetGenericArguments()[0];

					if (propertyType.IsGenericType &&
						propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
					{
						listNullableTypes.Add(propertyInfo.Name);
					}
				}
			}

			if (0 == listNullableTypes.Count) return;

			// we have errors...
			var errorMessage = string.Format(Resources.BaseStepRow_NullableTypesCheck, string.Join(Constants.CommaSeparator, listNullableTypes), type);

			// the assertion will throw an exception if this is invalid
			listNullableTypes.Count.Should().Be(0, errorMessage);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="type"></param>
		/// <param name="propertyType"></param>
		/// <param name="key"></param>
		/// <returns></returns>
		protected MethodInfo GetMethodInfo(Type type, Type propertyType, string key)
		{
			if (SimpleTypes.Contains(propertyType)) return null;

			var methodInfo = (MethodInfo)null;

			if (propertyType.IsEnum)
			{
				// handle using reflection to get the correct type
				methodInfo = _methodInfoEnum.MakeGenericMethod(propertyType);
			}
			else
			{
				// make sure the interface is implemented
				foreach (var iface in propertyType.GetInterfaces())
				{
					if (!iface.IsGenericType) continue;

					var typeOfInterface = iface.GetGenericTypeDefinition();
					var hasInterface = (typeOfInterface == typeof(IConvertibleFromString<>));
					var hasStaticInterface = (typeOfInterface == typeof(IConvertibleStaticFromString<>));

					if (!hasInterface && !hasStaticInterface) continue;

					methodInfo = (hasStaticInterface)
						? _methodInfoObjectStatic.MakeGenericMethod(propertyType)
						: _methodInfoObject.MakeGenericMethod(propertyType);

					break;
				}
				methodInfo.Should().NotBeNull(Resources.BaseStepRow_MethodInfoConverterCheck, key, type);

				// ReSharper disable once InvertIf
				if (propertyType.IsClass)
				{
					var hasDefaultConstructor = (null != propertyType.GetConstructor(Type.EmptyTypes));
					hasDefaultConstructor.Should().BeTrue(Resources.BaseStepRow_MethodInfoConstructorCheck, key, type);
				}
			}
			return methodInfo;
		}

	}
}

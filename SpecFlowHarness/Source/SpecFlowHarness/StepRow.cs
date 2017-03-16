using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using TechTalk.SpecFlow;

namespace CIAndT.SpecFlowHarness
{
	public abstract class StepRow<TStepRow> : BaseStepRow where TStepRow : BaseStepRow
	{
		private const string PropertyParseErrors = nameof(ParseErrors);
		private const string PropertyUsedProperties = nameof(UsedProperties);

		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// transform a SpecFlow table object into a class collection
		/// </summary>
		/// <typeparam name="TStepRow">the derived class</typeparam>
		/// <param name="table">table data provided by specflow</param>
		/// <returns>a list of objects of the defined type</returns>
		[StepArgumentTransformation]
		public IReadOnlyCollection<TStepRow> AutoTransform(Table table)
		{
			var collection = Transform(table);
			collection.Validate();
			return collection;
		}

		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// use reflection to perform the check for valid fields in the DERIVED class
		/// </summary>
		/// <param name="other">the other instance with which to compare</param>
		protected bool HelperEquals(TStepRow other)
		{
			var type = typeof(TStepRow);
			other.Should().NotBeNull(Resources.StepRow_EqualsOverrideNullMessage);

			var equals = true;

			// ReSharper disable once InvertIf
			if (!ReferenceEquals(this, other))
			{
				// ReSharper disable once PossibleNullReferenceException
				var usedProperties = UsedProperties.Union(other.UsedProperties).ToList();

				foreach (var propertyName in usedProperties)
				{
					var propertyInfo = type.GetProperty(propertyName);

					// this should perform a check against the type, not the object
					dynamic thisValue = propertyInfo.GetValue(this, null);
					dynamic otherValue = propertyInfo.GetValue(other, null);

					var isCollection = propertyInfo.PropertyType.IsConstructedGenericType
						&& typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType);

					if (!isCollection)
					{
						equals = (thisValue == otherValue);
						if (!equals) break;
					}
					else // check that the collections' contents are equal
					{
						var bothNull = (thisValue == null && otherValue == null);
						var bothNotNull = (thisValue != null && otherValue != null);

						equals = bothNull || bothNotNull;
						if (!equals) break;
						if (bothNull) break;

						// ReSharper disable PossibleNullReferenceException
						equals = (thisValue.Count == otherValue.Count);
						// ReSharper restore PossibleNullReferenceException
						if (!equals) break;

						//equals = Enumerable.SequenceEqual(thisValue, otherValue);
						//if (!equals) break;

						for (var index = 0; index < thisValue.Count; ++index)
						{
							equals = (otherValue.Contains(thisValue[index]));
							if (!equals) break;
						}
						if (!equals) break;

						for (var index = 0; index < otherValue.Count; ++index)
						{
							equals = (thisValue.Contains(otherValue[index]));
							if (!equals) break;
						}
						if (!equals) break;
					}
				}
			}
			return equals;
		}

		/// <summary>
		/// use reflection to perform the check for valid fields
		/// </summary>
		protected int HelperHashCode()
		{
			var type = typeof(TStepRow);
			var baseType = typeof(StepRow<TStepRow>);

			// we only want the properties from the derived class
			var baseProperties = baseType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			var derivedProperties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);

			var basePropertyNames = baseProperties.Select(p => p.Name);
			var derivedPropertyNames = derivedProperties.Select(p => p.Name);

			var propertyNames = derivedPropertyNames.Except(basePropertyNames).ToList();

			int hash;

			unchecked
			{
				hash = type.GetHashCode();

				foreach (var propertyName in propertyNames)
				{
					var propertyInfo = type.GetProperty(propertyName);
					dynamic value = propertyInfo.GetValue(this, null);

					hash = (hash * Constants.HashPrime) ^ value.GetHashCode();
				}
			}
			return hash;
		}

		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="TProperty">property type</typeparam>
		/// <param name="expression">a lambda expression of the type s => s.PropertyName</param>
		/// <returns>if this property is populated</returns>
		protected bool IsUsedProperty<TProperty>(Expression<Func<TStepRow, TProperty>> expression)
		{
			var uses = false;
			var memberExpression = expression.Body as MemberExpression;

			// ReSharper disable once InvertIf
			if (memberExpression != null &&
				memberExpression.Member.MemberType == MemberTypes.Property)
			{
				var sPropertyName = memberExpression.Member.Name;
				uses = (UsedProperties.Contains(sPropertyName));
			}
			return uses;
		}

		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// validates that the specified int has a valid value (not 0)
		/// </summary>
		/// <param name="value">the value to be validated against 0</param>
		/// <param name="expression">a lambda expression of the type s => s.PropertyName</param>
		protected void Validate_Int32(int value, Expression<Func<TStepRow, int>> expression)
		{
			if (0 != value) return;

			var sPropertyName = string.Empty;
			var memberExpression = expression.Body as MemberExpression;

			if (memberExpression != null &&
				memberExpression.Member.MemberType == MemberTypes.Property)
			{
				sPropertyName = memberExpression.Member.Name;
			}
			var sParseError = string.Format(Resources.StepRow_ValidationFieldNotSet, sPropertyName, typeof(int));
			ParseErrors.Add(sParseError);
		}

		/// <summary>
		/// validates that the specified long has a valid value (not 0)
		/// </summary>
		/// <param name="value">the value to be validated against 0</param>
		/// <param name="expression">a lambda expression of the type s => s.PropertyName</param>
		protected void Validate_Int64(long value, Expression<Func<TStepRow, long>> expression)
		{
			if (0 != value) return;

			var sPropertyName = string.Empty;
			var memberExpression = expression.Body as MemberExpression;

			if (memberExpression != null &&
				memberExpression.Member.MemberType == MemberTypes.Property)
			{
				sPropertyName = memberExpression.Member.Name;
			}
			var sParseError = string.Format("The {1} for '{0}' was not set.", sPropertyName, typeof(long));
			ParseErrors.Add(sParseError);
		}

		/// <summary>
		/// validates that the specified decimal has a valid value (not 0)
		/// </summary>
		/// <param name="value">the value to be validated against 0</param>
		/// <param name="expression">a lambda expression of the type s => s.PropertyName</param>
		protected void Validate_Decimal(decimal value, Expression<Func<TStepRow, decimal>> expression)
		{
			if (0 != value) return;

			var sPropertyName = string.Empty;
			var memberExpression = expression.Body as MemberExpression;

			if (memberExpression != null &&
				memberExpression.Member.MemberType == MemberTypes.Property)
			{
				sPropertyName = memberExpression.Member.Name;
			}
			var sParseError = string.Format("The {1} for '{0}' was not set.", sPropertyName, typeof(decimal));
			ParseErrors.Add(sParseError);
		}

		/// <summary>
		/// validates that the specified Date has a valid value (not DateTime.MinValue)
		/// </summary>
		/// <param name="value">the value to be validated against DateTime.MinValue</param>
		/// <param name="expression">a lambda expression of the type s => s.PropertyName</param>
		protected void Validate_DateTime(DateTime value, Expression<Func<TStepRow, DateTime>> expression)
		{
			if (DateTime.MinValue != value) return;

			var sPropertyName = string.Empty;
			var memberExpression = expression.Body as MemberExpression;

			if (memberExpression != null &&
				memberExpression.Member.MemberType == MemberTypes.Property)
			{
				sPropertyName = memberExpression.Member.Name;
			}
			var sParseError = string.Format("The {1} for '{0}' was not set.", sPropertyName, typeof(DateTime));
			ParseErrors.Add(sParseError);
		}

		/// <summary>
		/// validates that the specified Date has a valid value (not DateTime.MinValue)
		/// </summary>
		/// <param name="value">the value to be validated against DateTime.MinValue</param>
		/// <param name="expression">a lambda expression of the type s => s.PropertyName</param>
		protected void Validate_String(string value, Expression<Func<TStepRow, string>> expression)
		{
			if (!string.IsNullOrWhiteSpace(value)) return;

			var sPropertyName = string.Empty;
			var memberExpression = expression.Body as MemberExpression;

			if (memberExpression != null &&
				memberExpression.Member.MemberType == MemberTypes.Property)
			{
				sPropertyName = memberExpression.Member.Name;
			}
			var sParseError = string.Format("The {1} for '{0}' was not set.", sPropertyName, typeof(string));
			ParseErrors.Add(sParseError);
		}

		// ----------------------------------------------------------------------------------------

		/// <summary>
		/// take a SpecFlow table and transform it to the supplied type
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		private IReadOnlyCollection<TStepRow> Transform(Table table)
		{
			// make sure we have valid column names
			// and that we are not using nullable types
			CheckForMissingColumnsOnClass<TStepRow>(table);
			CheckForNullableTypesOnClass<TStepRow>();

			var type = typeof(TStepRow);
			var entries = new Collection<TStepRow>();
			var typeInfo = type.GetTypeInfo();

			// handle specific type conversions and default the others
			foreach (var row in table.Rows)
			{
				var entry = (TStepRow)Activator.CreateInstance(type);
				entry.Should().NotBeNull(Resources.StepRow_CouldNotCreateInstance, type);

				var keys = row.Keys.ToArray();
				var values = row.Values.ToArray();

				// we need to reference the error structure property
				// we need to reference the used property structure property
				var propertyInfoParseErrors = typeInfo.GetProperty(PropertyParseErrors);
				var propertyInfoUsedProperties = typeInfo.GetProperty(PropertyUsedProperties);

				propertyInfoParseErrors.Should().NotBeNull(Resources.StepRow_CollectionPropertyNotFound, PropertyParseErrors);
				propertyInfoUsedProperties.Should().NotBeNull(Resources.StepRow_CollectionPropertyNotFound, PropertyUsedProperties);

				dynamic listParseErrors = propertyInfoParseErrors.GetValue(entry);
				dynamic listUsedProperties = propertyInfoUsedProperties.GetValue(entry);

				// loop through all columns in the table and map them to our class
				for (var column = 0; column < row.Count; ++column)
				{
					// strings are non-null and trimmed
					var key = keys[column];
					var value = values[column];

					var sNarrowKey = key.Replace(Constants.Space, string.Empty);
					var propertyInfo = typeInfo.GetProperty(sNarrowKey, BindingFlags.Public | BindingFlags.Instance);

					// note that this column was populated in the table
					listUsedProperties.Add(propertyInfo.Name);

					if (string.Empty == value) continue;

					// we have data that we need to convert
					var isCollection = propertyInfo.PropertyType.IsConstructedGenericType &&
										typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType);

					// if a class, struct or enum then we need the methodInfo structure populated accordingly
					var propertyType = (isCollection) ? propertyInfo.PropertyType.GetGenericArguments()[0] : propertyInfo.PropertyType;
					var methodInfo = GetMethodInfo(type, propertyType, key);

					if (!isCollection)
					{
						var sParseError = string.Empty;
						var result = ParseValue(propertyType, value, key, methodInfo, ref sParseError);

						if (string.IsNullOrEmpty(sParseError))
						{
							propertyInfo.SetValue(entry, result);
							continue;
						}

						// pick up any error encountered during parsing
						listParseErrors.Add(sParseError);
					}
					else
					{
						// we expect a collection, so split the string into separate entries
						var listValues = value.Split(new[] { Constants.CommaSeparator }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();

						// create a collection using reflection
						var collectionType = typeof(Collection<>).MakeGenericType(propertyType);
						dynamic collection = Activator.CreateInstance(collectionType);

						var sParseError = string.Empty;

						foreach (var nextValue in listValues)
						{
							var result = ParseValue(propertyType, nextValue, key, methodInfo, ref sParseError);

							if (string.IsNullOrEmpty(sParseError))
							{
								collection.Add(result);
								continue;
							}

							// pick up any error encountered during parsing
							listParseErrors.Add(sParseError);
						}
						propertyInfo.SetValue(entry, collection);
					}
				}
				entries.Add(entry);
			}
			return entries;
		}

		private dynamic ParseValue(Type propertyType, string value, string key, MethodInfo methodInfo, ref string sParseError)
		{
			dynamic result;

			if (propertyType == typeof(string))
			{
				result = value;
			}
			else if (propertyType == typeof(bool))
			{
				result = Parser.ParseBoolean(key, value, ref sParseError);
			}
			else if (propertyType == typeof(short))
			{
				result = Parser.ParseInt16(key, value, ref sParseError);
			}
			else if (propertyType == typeof(int))
			{
				result = Parser.ParseInt32(key, value, ref sParseError);
			}
			else if (propertyType == typeof(long))
			{
				result = Parser.ParseInt64(key, value, ref sParseError);
			}
			else if (propertyType == typeof(DateTime))
			{
				result = Parser.ParseDateTime(key, value, ref sParseError);
			}
			else if (SimpleTypes.Contains(propertyType))
			{
				result = Parser.ParseValue(propertyType, key, value, ref sParseError);
			}
			else    // enum, class or struct
			{
				var parameters = new object[] { key, value, sParseError };
				result = methodInfo.Invoke(Parser, parameters);
				sParseError = (string)parameters[2];
			}
			return result;
		}

		// ----------------------------------------------------------------------------------------
	}
}

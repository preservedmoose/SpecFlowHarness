using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using TechTalk.SpecFlow;

namespace PreservedMoose.SpecFlowHarness.AcceptanceTests.StepRows
{
	// ReSharper disable BuiltInTypeReferenceStyle
	// ReSharper disable UnusedAutoPropertyAccessor.Local
	[Binding]
	public sealed class StepTestNullableRow : StepRow<StepTestNullableRow>
	{
		// values
		public Int32? NullableInt32Value { get; private set; }

		// lists
		public IReadOnlyCollection<Int32?> NullableInt32Values { get; private set; }

		public StepTestNullableRow()
		{
			NullableInt32Values = new Collection<Int32?>();
		}
	}
	// ReSharper restore UnusedAutoPropertyAccessor.Local
	// ReSharper restore BuiltInTypeReferenceStyle
}

using System;

namespace PreservedMoose.SpecFlowHarness
{
	internal class StepRowAssertion<T>
	{
		private readonly Type _type;
		private readonly T _actual;

		public StepRowAssertion(T actual)
		{
			_type = typeof(T);
			_actual = actual;
		}

		public void Be(T expected, string reason, params object[] reasonArguments)
		{
			if (!_actual.Equals(expected)) throw new StepRowException(string.Format(reason, reasonArguments));
		}

		public void BeNull(string reason, params object[] reasonArguments)
		{
			if (_actual is ValueType) throw new InvalidCastException();

			// ReSharper disable once CompareNonConstrainedGenericWithNull
			if (_actual != null) throw new StepRowException(string.Format(reason, reasonArguments));
		}

		public void NotBeNull(string reason, params object[] reasonArguments)
		{
			if (_actual is ValueType) throw new InvalidCastException();

			// ReSharper disable once CompareNonConstrainedGenericWithNull
			if (_actual == null) throw new StepRowException(string.Format(reason, reasonArguments));
		}

		public void BeTrue(string reason, params object[] reasonArguments)
		{
			if (_type != typeof(bool)) throw new InvalidCastException();

			if (!Convert.ToBoolean(_actual)) throw new StepRowException(string.Format(reason, reasonArguments));
		}

		public void BeFalse(string reason, params object[] reasonArguments)
		{
			if (_actual.GetType() != typeof(bool)) throw new InvalidCastException();

			if (Convert.ToBoolean(_actual)) throw new StepRowException(string.Format(reason, reasonArguments));
		}
	}
}

﻿using System.ComponentModel;

namespace PreservedMoose.SpecFlowHarness.UnitTests.TestClasses
{
	public enum Colour
	{
		None = 0,
		[Description("Red Colour")]
		Red,
		[Description("Green Colour")]
		Green,
		[Description("Blue Colour")]
		Blue,
		[Description("Orange Colour")]
		Orange,
		[Description("Purple Colour")]
		Purple,
		[Description("Cyan Colour")]
		Cyan
	}
}

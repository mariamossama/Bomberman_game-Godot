// GdUnit generated TestSuite
using Godot;
using GdUnit4;

using Godot;
using System;

namespace mynamespaceCheck
{
	using static Assertions;
	using static Utils;

	[TestSuite]
	public class BoxTest
	{
		[TestCase]
		public void Destroy()
		{
			
			var mybox = new Box();
			mybox.Destroy();
			bool res = mybox.getIsDestroyed();
			AssertBool(res).IsEqual(true);

		}
		
		[TestCase]
		public void newcheck()
		{
			var mybox = new Box();
			bool res = mybox.getIsDestroyed();
			
			AssertBool(res).IsEqual(false);

		}
	}
}

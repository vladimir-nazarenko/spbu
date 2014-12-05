using System;
using NUnit.Framework;
using zero;

namespace zero
{
	[TestFixture()]
	public class ZeroTest
	{
		[Test()]
		public void TestCase ()
		{
			int[] data = new int[5];
			for (int i = 0; i < 5; i++)
				data[i] = i;
			data[2] = 0;
			data[4] = 0;
			MainClass main = new MainClass();
			Assert.AreEqual(3, main.zeroCount(data));
		}

		[Test()]
		public void OneElement ()
		{
			int[] data = new int[1];
			data[0] = 0;
			MainClass main = new MainClass();
			Assert.AreEqual(1, main.zeroCount(data));
		}
	}
}


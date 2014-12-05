using System;
using System.Linq;

namespace zero
{
	class MainClass
	{
		public static void Main (string[] args)
		{
		}

		public int zeroCount (int[] array)
		{
			int count = 0;
			for (int i = 0; i < array.Length; i++)
				if (array[i] == 0)
					count++;
			return count;
		}
	}
}

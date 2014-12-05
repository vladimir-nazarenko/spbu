using System;
using NUnit.Framework;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Robots
{
	[TestFixture()]
	public class GraphTests
	{
		[Test()]
		public void TestCase ()
		{
			int [, ] matrix = Matrix("input.txt");
			List<int> robotsInitial = new List<int>{0, 2, 4};
			Graph graph = new Graph(matrix);
			int painted = 0;
			foreach (int item in robotsInitial) {
				if(graph.isPainted(item))
					painted++;
			}
			Assert.IsFalse(painted == 1 || robotsInitial.Count - painted == 1);

		}
		[Test()]
		public void Triangle ()
		{
			int [, ] matrix = Matrix("input1.txt");
			List<int> robotsInitial = new List<int>{0, 1};
			Graph graph = new Graph(matrix);
			int painted = 0;
			foreach (int item in robotsInitial) {
				if(graph.isPainted(item))
					painted++;
			}
			//Assert.AreEqual();
			//Assert.IsFalse(painted == 1 || robotsInitial.Count - painted == 1);
		}

		public static int[, ] Matrix (String path)
		{
			StreamReader sr = new StreamReader (path);
			int dimensions = int.Parse(sr.ReadLine());
			int [, ] matrix = new int[dimensions, dimensions];
			for (int i = 0; i < matrix.GetLongLength(0); i++) {
				String[] array = sr.ReadLine().Split(',').ToArray();
				for (int j = 0; j < matrix.GetLongLength(0); j++)
				{
					matrix[i, j] = Int32.Parse(array[j]);
				}
			}
			return matrix;
		}
	}
}


using System;
using System.IO;
using System.Linq;

namespace Robots
{
	public class Graph
	{
		public Graph (int[, ] matrix)
		{
			mMatrix = matrix;
			painted = new bool[mMatrix.GetLongLength(0)];
			paint (0, 0, 0, false);
		}

		// change matrix painted to split graph
		public void paint (int initial, int ancestor, int start, bool skip)
		{
			if (!skip)
				painted [start] = true;
			for (int i = 0; i < mMatrix.GetLongLength(0); i++) {
				if (i == ancestor || i == initial) continue;
				if (i != start && mMatrix[start, i] != 0)
					paint (initial, start, i, !skip);
			}
		}

		// check if vertice is from the first part
		public bool isPainted(int vertice)
		{
			return painted[vertice];
		}

		private bool[] painted;
		private int [, ] mMatrix;
	}
}


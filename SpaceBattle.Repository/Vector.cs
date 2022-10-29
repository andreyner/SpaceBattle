using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceBattle.Repository
{
	public class Vector
	{
		private int[] points;

		public Vector(int[] items)
		{
			this.points = items;
		}

		public static Vector operator +(Vector v1, Vector v2)
		{
			return new Vector(v1.points.Zip(v2.points, (x, y) => x + y).ToArray());
		}

		public static bool operator ==(Vector v1, Vector v2)
		{
			return v1.points.SequenceEqual(v2.points);
		}

		public static bool operator !=(Vector v1, Vector v2)
		{
			return !v1.points.SequenceEqual(v2.points);
		}

		public int this[int index]
		{
			get => points[index];
			set => points[index] = value;
		}
	}
}

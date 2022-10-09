using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceBattle.Repository
{
	public class Vector
	{
		private int[] items;

		public Vector(int[] items)
		{
			this.items = items;
		}
		public static Vector Add(Vector v1, Vector v2)
		{
			return new Vector(v1.items.Zip(v2.items, (x, y) => x + y).ToArray());
		}

		public static bool operator ==(Vector v1, Vector v2)
		{
			return v1.items.SequenceEqual(v2.items);
		}

		public static bool operator !=(Vector v1, Vector v2)
		{
			return !v1.items.SequenceEqual(v2.items);
		}

		public int this[int index]
		{
			get => items[index];
			set => items[index] = value;
		}
	}
}

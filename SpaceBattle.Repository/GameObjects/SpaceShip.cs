using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.GameObjects
{
	public class SpaceShip : Uobject
	{
		public override object this[string key]
		{
			get
			{
				return values[key];
			}

			set
			{
				values.Add(key, value);
			}
		}
	}
}

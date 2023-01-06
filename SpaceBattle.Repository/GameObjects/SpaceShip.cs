using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.GameObjects
{
	public class SpaceShip : Uobject
	{
		public override object this[string key]
		{
			get => this[key];
			set => this[key] = value;
		}
	}
}

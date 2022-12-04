using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository
{
	public class MovableAdapter2 : IMovable
	{
		Uobject _obj;

		public MovableAdapter2(Uobject obj)
		{
			_obj = obj;
		}

		public Vector Position
		{
			get { return (Vector)_obj["position"]; }
			set
			{
				_obj["position"] = value;
			}
		}

		public Vector Velocity => (Vector)_obj["velocity"];

	}
}

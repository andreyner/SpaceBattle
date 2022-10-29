using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository
{
	public class MovableAdapter : IMovable
	{
		Uobject _obj;

		public MovableAdapter(Uobject obj)
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

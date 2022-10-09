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

		public Vector GetPosition()
		{
			return (Vector)_obj["position"];
		}

		public Vector GetVelocity()
		{
			return (Vector)_obj["velocity"];
		}
		

		public void SetPosition(Vector newValue)
		{
			_obj["position"] = newValue;
		}
	}
}

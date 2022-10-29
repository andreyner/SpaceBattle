using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository
{
	public class RotableAdapter : IRotable
	{
		Uobject _obj;

		public RotableAdapter(Uobject obj)
		{
			_obj = obj;
		}

		public int Direction
		{
			get { return (int)_obj["direction"]; }
			set
			{
				_obj["direction"] = value;
			}
		}

		public int DirectionsNumber
		{
			get { return (int)_obj["directionNumber"]; }
		}

		public int AngularVelocity
		{
			get { return (int)_obj["angularVelocity"]; }
		}

	}
}

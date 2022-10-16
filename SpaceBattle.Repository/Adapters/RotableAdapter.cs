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

		public Vector[] Position
		{
			get
			{
				return (Vector[])_obj["turningPointers"];
			}
			set
			{
				_obj["turningPointers"] = value;
			}
		}

		public Vector PivotPointer
		{
			get
			{
				return (Vector)_obj["pivotPointer"];
			}
			set
			{
				_obj["pivotPointer"] = value;
			}
		}

		public int Angle
		{
			get
			{
				return (int)_obj["rotationAngle"];
			}
			set
			{
				_obj["rotationAngle"] = value;
			}
		}
	}
}

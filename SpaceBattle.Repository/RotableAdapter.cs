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

		public int GetAngle()
		{
			return (int)_obj["rotationAngle"];
		}

		public Vector GetPivotPointer()
		{
			return (Vector)_obj["pivotPointer"];
		}

		public Vector[] GetPosition()
		{
			return (Vector[])_obj["turningPointers"];
		}

		public void SetAngle(int angle)
		{
			_obj["rotationAngle"] = angle;
		}

		public void SetPosition(Vector[] newValue)
		{
			_obj["turningPointers"] = newValue;
		}
	}
}

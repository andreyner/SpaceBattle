using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository
{
	public class Rotate: ICommand
	{
		IRotable _rotable;

		public Rotate(IRotable rotable)
		{
			_rotable = rotable;
		}

		public void Execute()
		{
			_rotable.Direction = (_rotable.Direction + _rotable.AngularVelocity) % _rotable.DirectionsNumber;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository
{
	public class Move
	{
		IMovable _movable;

		public Move (IMovable movable)
		{
			_movable = movable;
		}

		public void Execute()
		{
			_movable.SetPosition(
				 Vector.Add(_movable.GetPosition(), _movable.GetVelocity())
				);
		}
	}
}

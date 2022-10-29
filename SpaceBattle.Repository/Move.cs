using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository
{
	public class Move: ICommand
	{
		IMovable _movable;

		public Move (IMovable movable)
		{
			_movable = movable;
		}

		public void Execute()
		{
			_movable.Position += _movable.Velocity;
	
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository
{
	public interface IMovable
	{
		Vector Position { get; set; }

		Vector Velocity { get; }

	}

}

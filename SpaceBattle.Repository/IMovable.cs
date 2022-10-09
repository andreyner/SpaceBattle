using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository
{
	public interface IMovable
	{
		Vector GetPosition();
		Vector GetVelocity();
		void SetPosition(Vector newValue);
	}
}

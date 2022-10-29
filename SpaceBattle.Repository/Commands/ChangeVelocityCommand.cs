using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Commands
{
	/// <summary>
	/// Команда для модификации вектора мгновенной скорости при повороте
	/// </summary>
	public class ChangeVelocityCommand : ICommand
	{
		private readonly IChangeVelocity _changeVelocity;

		public ChangeVelocityCommand(IChangeVelocity changeVelocity)
		{
			_changeVelocity = changeVelocity;
		}

		public void Execute()
		{
			_changeVelocity.Velocity[0] = (int)(_changeVelocity.Velocity[0] * Math.Cos(2 * Math.PI * (_changeVelocity.Direction % _changeVelocity.DirectionsNumber)));
			_changeVelocity.Velocity[1] = (int)(_changeVelocity.Velocity[1] * Math.Sin(2 * Math.PI * (_changeVelocity.Direction % _changeVelocity.DirectionsNumber)));
		}
	}
}

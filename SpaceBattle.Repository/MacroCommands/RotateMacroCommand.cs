using SpaceBattle.Repository.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.MacroCommands
{
	/// <summary>
	/// Команда поворота, которая еще и меняет вектор мгновенной скорости
	/// </summary>
	public class RotateMacroCommand : ICommand
	{
		private readonly Rotate _rotate;
		private readonly ChangeVelocityCommand _changeVelocity;

		public RotateMacroCommand(Rotate rotate, ChangeVelocityCommand changeVelocity)
		{
			this._rotate = rotate;
			this._changeVelocity = changeVelocity;
		}

		public void Execute()
		{
			new MacroComamnd(new ICommand[] {_rotate, _changeVelocity }).Execute();
		}
	}
}

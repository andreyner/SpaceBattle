using SpaceBattle.Repository.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Commands
{
	public class CheckFuel : ICommand
	{
		private readonly ICheckFuel _checkFuel;

		public CheckFuel(ICheckFuel checkFuel)
		{
			_checkFuel = checkFuel;
		}

		public void Execute()
		{
			if (_checkFuel.FuelVolume - _checkFuel.FuelExpense < 0)
			{
				throw new CommandException("Недостаточно топлива!");
			}
		}
	}
}

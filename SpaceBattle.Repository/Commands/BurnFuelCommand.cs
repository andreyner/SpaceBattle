using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Commands
{
	public class BurnFuel : ICommand
	{
		private readonly IBurnFuel _burnFuel;

		public BurnFuel(IBurnFuel burnFuel)
		{
			_burnFuel = burnFuel;
		}

		public void Execute()
		{
			_burnFuel.FuelVolume = _burnFuel.FuelVolume - _burnFuel.FuelExpense;
		}
	}
}

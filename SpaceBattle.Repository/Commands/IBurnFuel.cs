using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Commands
{
	public interface IBurnFuel
	{
		int FuelVolume { get; set; }

		int FuelExpense { get; }
	}
}

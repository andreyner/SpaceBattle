using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Commands
{
	public interface ICheckFuel
	{
		int FuelVolume { get; }
	}
}

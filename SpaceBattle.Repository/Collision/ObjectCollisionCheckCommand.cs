using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Collision
{
	/// <summary>
	/// Команда проверки столкновений двух объектов
	/// </summary>
	public class ObjectCollisionCheckCommand : ICommand
	{
		private Uobject SectorSpaceShip;
		private Uobject CurrentSpaceShip;

		public ObjectCollisionCheckCommand(Uobject sectorSpaceShip, Uobject currentSpaceShip)
		{
			SectorSpaceShip = sectorSpaceShip;
			CurrentSpaceShip = currentSpaceShip;
		}

		public void Execute()
		{
		}
	}
}

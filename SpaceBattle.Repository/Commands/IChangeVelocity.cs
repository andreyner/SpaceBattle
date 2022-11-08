using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Commands
{
	public interface IChangeVelocity
	{
		/// <summary>
		/// Текущее направление
		/// </summary>
		int Direction { get; set; }

		/// <summary>
		/// Количество направлений
		/// </summary>
		int DirectionsNumber { get; }

		/// <summary>
		/// Мгновенная скорость
		/// </summary>
		Vector Velocity { get; set; }
	}
}

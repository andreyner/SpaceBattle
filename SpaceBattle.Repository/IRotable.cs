using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository
{
	public interface IRotable
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
		/// Угловая скорость
		/// </summary>
		int AngularVelocity { get; }
	}
}

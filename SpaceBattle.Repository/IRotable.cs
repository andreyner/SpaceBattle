using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository
{
	public interface IRotable
	{
		Vector[] Position { get; set; }

		/// <summary>
		/// Возвращает точу относительно которой поворачивают точки объекта
		/// </summary>
		Vector PivotPointer { get; }

		/// <summary>
		/// Получить угол поворота
		/// </summary>
		int Angle { get; set; }

	}
}

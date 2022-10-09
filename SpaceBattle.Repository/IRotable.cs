using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository
{
	public interface IRotable
	{
		Vector[] GetPosition();

		void SetPosition(Vector[] newValue);

		/// <summary>
		/// Возвращает точу относительно которой поворачивают точки объекта
		/// </summary>
		Vector GetPivotPointer();

		/// <summary>
		/// Получить угол поворота
		/// </summary>
		int GetAngle();

		/// <summary>
		/// Установтть угол поворота
		/// </summary>
		void SetAngle(int rateAngle);
	}
}

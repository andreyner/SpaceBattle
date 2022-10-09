﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository
{
	public class Rotate
	{
		IRotable _rotable;

		public Rotate(IRotable rotable)
		{
			_rotable = rotable;
		}

		public void Execute()
		{
			var turningPointers = _rotable.GetPosition();
			var rotationAngle = _rotable.GetAngle();
			var pivotPointer = _rotable.GetPivotPointer();// точка относительно которой поворачиваем

			var angleRadian = rotationAngle * Math.PI / 180; //переводим угол в радианы

			for (int i = 0; i < turningPointers.Length; i++)
			{
				turningPointers[i][0] = (int)((turningPointers[i][0] - pivotPointer[0]) * Math.Cos(angleRadian) - (turningPointers[i][1] - pivotPointer[1]) * Math.Sin(angleRadian) + pivotPointer[0]);
				turningPointers[i][1] = (int)((turningPointers[i][0] - pivotPointer[0]) * Math.Sin(angleRadian) + (turningPointers[i][1] - pivotPointer[1]) * Math.Cos(angleRadian) + pivotPointer[1]);
			}
			_rotable.SetPosition(turningPointers);
		}
	}
}

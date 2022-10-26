using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpaceBattle.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Tests
{
	[TestClass]
	public class RotateObjectTest
	{
		/// <summary>
		/// Для объекта, находящегося в 1-ом секторе и имеющего угловую скорость 9, новое положение меняется на 2ой сектор.Вся область имеет 8 возможных положений
		/// </summary>
		[TestMethod]
		public void RotateObject()
		{
			var rotableObject = new Mock<IRotable>();

			rotableObject.SetupProperty(x => x.Direction, 1);
			rotableObject.SetupGet(x => x.AngularVelocity).Returns(9);
			rotableObject.SetupGet(x => x.DirectionsNumber).Returns(8);

			var rotate = new Rotate(rotableObject.Object);
			rotate.Execute();

			rotableObject.VerifySet(x => x.Direction = It.Is<int>(x => x == 2), "Неверный результат перемещения!");

			Assert.IsTrue(true);

		}

		/// <summary>
		/// Попытка повернуть объект, у которого невозможно прочитать положение в пространстве, приводит к ошибке
		/// </summary>
		[TestMethod]
		public void CannotReadDirection()
		{
			var rotableObject = new Mock<IRotable>();

			rotableObject.SetupGet(x => x.Direction).Throws<Exception>();
			rotableObject.SetupGet(x => x.AngularVelocity).Returns(9);
			rotableObject.SetupGet(x => x.DirectionsNumber).Returns(8);

			var rotate = new Rotate(rotableObject.Object);

			Assert.ThrowsException<Exception>(() => rotate.Execute(), "Попытка повернуть объект, у которого невозможно прочитать положение в пространстве, удалась!");
		}

		/// <summary>
		/// Попытка повернуть объект, у которого невозможно прочитать угловую скорость, приводит к ошибке
		/// </summary>
		[TestMethod]
		public void CannotReadAngularVelocity()
		{
			var rotableObject = new Mock<IRotable>();

			rotableObject.SetupProperty(x => x.Direction, 1);
			rotableObject.SetupGet(x => x.AngularVelocity).Throws<Exception>();
			rotableObject.SetupGet(x => x.DirectionsNumber).Returns(8);

			var rotate = new Rotate(rotableObject.Object);

			Assert.ThrowsException<Exception>(() => rotate.Execute(), "Попытка повернуть объект, у которого невозможно прочитать угловую скорость, удалась!");
		}

		/// <summary>
		/// Попытка повернуть объект, у которого невозможно изменить положение в пространстве, приводит к ошибке
		/// </summary>
		[TestMethod]
		public void CannotChangePosition()
		{
			var rotableObject = new Mock<IRotable>();

			rotableObject.SetupProperty(x => x.Direction, 1);
			rotableObject.SetupGet(x => x.AngularVelocity).Throws<Exception>();
			rotableObject.SetupGet(x => x.DirectionsNumber).Returns(8);

			rotableObject.SetupSet(x => x.Direction = It.IsAny<int>()).Throws<Exception>();

			var rotate = new Rotate(rotableObject.Object);

			Assert.ThrowsException<Exception>(() => rotate.Execute(), "Попытка повернуть объект, у которого невозможно изменить положение в пространстве, удалась!");
		}
	}
}

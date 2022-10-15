using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpaceBattle.Repository;
using System;

namespace SpaceBattle.Tests
{
	[TestClass]
	public class MovementObjectTest
	{
		/// <summary>
		/// Для объекта, находящегося в точке (12, 5) и движущегося со скоростью (-7, 3) движение меняет положение объекта на (5, 8)
		/// </summary>
		[TestMethod]
		public void MoveObject()
		{
			var movableObject = new Mock<IMovable>();

			movableObject.SetupProperty(x => x.Position, new Vector(new int[] { 12, 5 }));
			movableObject.SetupGet(x => x.Velocity).Returns(() => new Vector(new int[] { -7, 3 }));
		
			var move = new Move(movableObject.Object);
			move.Execute();

			movableObject.VerifySet(x => x.Position = It.Is<Vector>(c => c == new Vector(new int[] { 5, 8 })), "Неверный результат перемещения!");

			Assert.IsTrue(true);

		}

		/// <summary>
		/// Попытка сдвинуть объект, у которого невозможно прочитать положение в пространстве, приводит к ошибке
		/// </summary>
		[TestMethod]
		public void CannotReadPosition()
		{
			var movableObject = new Mock<IMovable>();

			movableObject.SetupGet(x => x.Velocity).Returns(() => new Vector(new int[] { -7, 3 }));
			movableObject.SetupGet(x => x.Position).Throws<Exception>();

			var move = new Move(movableObject.Object);

			Assert.ThrowsException<Exception>(() => move.Execute(), "Попытка сдвинуть объект, у которого невозможно прочитать положение в пространстве, удалась!");
		}

		/// <summary>
		/// Попытка сдвинуть объект, у которого невозможно прочитать значение мгновенной скорости, приводит к ошибке
		/// </summary>
		[TestMethod]
		public void CannotReadVelocity()
		{
			var movableObject = new Mock<IMovable>();

			movableObject.SetupProperty(x => x.Position, new Vector(new int[] { 12, 5 }));
			movableObject.SetupGet(x => x.Velocity).Throws<Exception>();

			var move = new Move(movableObject.Object);

			Assert.ThrowsException<Exception>(() => move.Execute(), "Попытка сдвинуть объект, у которого невозможно прочитать значение мгновенной скорости, удалась!");
		}


		/// <summary>
		/// Попытка сдвинуть объект, у которого невозможно изменить положение в пространстве, приводит к ошибкее
		/// </summary>
		[TestMethod]
		public void CannotChangePosition()
		{
			var movableObject = new Mock<IMovable>();

			movableObject.SetupProperty(x => x.Position, new Vector(new int[] { 12, 5 }));
			movableObject.SetupGet(x => x.Velocity).Returns(() => new Vector(new int[] { -7, 3 }));

			movableObject.SetupSet(x => x.Position = It.IsAny<Vector>()).Throws<Exception>();

			var move = new Move(movableObject.Object);

			Assert.ThrowsException<Exception>(() => move.Execute(), "Попытка сдвинуть объект, у которого невозможно изменить положение в пространстве, удалась!");
		}
	}
}
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
			var movableObject = new Mock<Uobject>();
			Vector finalPosition = null;

			movableObject.SetupGet(x => x["position"]).Returns(() => new Vector(new int[] { 12, 5 }));
			movableObject.SetupGet(x => x["velocity"]).Returns(() => new Vector(new int[] { -7, 3 }));

			movableObject.SetupSet(x => x["position"] = It.IsAny<Vector>()).Callback<string, object>((key, value) => finalPosition = (Vector) value);

			var movableAdapter = new MovableAdapter(movableObject.Object);
			var move = new Move(movableAdapter);

			move.Execute();

			Assert.IsTrue(finalPosition == new Vector(new int[] { 5, 8 }),"Неверный результат перемещения!");
		}

		/// <summary>
		/// Попытка сдвинуть объект, у которого невозможно прочитать положение в пространстве, приводит к ошибке
		/// </summary>
		[TestMethod]
		public void CannotReadPosition()
		{
			var movableObject = new Mock<Uobject>();

			movableObject.SetupGet(x => x["velocity"]).Returns(() => new Vector(new int[] { -7, 3 }));
			movableObject.SetupGet(x => x["position"]).Throws<Exception>();

			var movableAdapter = new MovableAdapter(movableObject.Object);
			var move = new Move(movableAdapter);

			Assert.ThrowsException<Exception>(() => move.Execute(), "Попытка сдвинуть объект, у которого невозможно прочитать положение в пространстве, удалась!");
		}

		/// <summary>
		/// Попытка сдвинуть объект, у которого невозможно прочитать значение мгновенной скорости, приводит к ошибке
		/// </summary>
		[TestMethod]
		public void CannotReadVelocity()
		{
			var movableObject = new Mock<Uobject>();

			movableObject.SetupGet(x => x["position"]).Returns(() => new Vector(new int[] { 12, 5 }));
			movableObject.SetupGet(x => x["velocity"]).Throws<Exception>();

			var movableAdapter = new MovableAdapter(movableObject.Object);
			var move = new Move(movableAdapter);

			Assert.ThrowsException<Exception>(() => move.Execute(), "Попытка сдвинуть объект, у которого невозможно прочитать значение мгновенной скорости, удалась!");
		}


		/// <summary>
		/// Попытка сдвинуть объект, у которого невозможно изменить положение в пространстве, приводит к ошибкее
		/// </summary>
		[TestMethod]
		public void CannotChangePosition()
		{
			var movableObject = new Mock<Uobject>();
			movableObject.SetupGet(x => x["position"]).Returns(() => new Vector(new int[] { 12, 5 }));
			movableObject.SetupGet(x => x["velocity"]).Returns(() => new Vector(new int[] { -7, 3 }));

			movableObject.SetupSet(x => x["position"] = It.IsAny<Vector>()).Throws<Exception>();

			var movableAdapter = new MovableAdapter(movableObject.Object);
			var move = new Move(movableAdapter);

			Assert.ThrowsException<Exception>(() => move.Execute(), "Попытка сдвинуть объект, у которого невозможно прочитать значение мгновенной скорости, удалась!");
		}
	}
}
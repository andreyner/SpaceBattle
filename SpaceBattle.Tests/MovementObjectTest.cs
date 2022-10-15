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
		/// ��� �������, ������������ � ����� (12, 5) � ����������� �� ��������� (-7, 3) �������� ������ ��������� ������� �� (5, 8)
		/// </summary>
		[TestMethod]
		public void MoveObject()
		{
			var movableObject = new Mock<IMovable>();

			movableObject.SetupProperty(x => x.Position, new Vector(new int[] { 12, 5 }));
			movableObject.SetupGet(x => x.Velocity).Returns(() => new Vector(new int[] { -7, 3 }));
		
			var move = new Move(movableObject.Object);
			move.Execute();

			movableObject.VerifySet(x => x.Position = It.Is<Vector>(c => c == new Vector(new int[] { 5, 8 })), "�������� ��������� �����������!");

			Assert.IsTrue(true);

		}

		/// <summary>
		/// ������� �������� ������, � �������� ���������� ��������� ��������� � ������������, �������� � ������
		/// </summary>
		[TestMethod]
		public void CannotReadPosition()
		{
			var movableObject = new Mock<IMovable>();

			movableObject.SetupGet(x => x.Velocity).Returns(() => new Vector(new int[] { -7, 3 }));
			movableObject.SetupGet(x => x.Position).Throws<Exception>();

			var move = new Move(movableObject.Object);

			Assert.ThrowsException<Exception>(() => move.Execute(), "������� �������� ������, � �������� ���������� ��������� ��������� � ������������, �������!");
		}

		/// <summary>
		/// ������� �������� ������, � �������� ���������� ��������� �������� ���������� ��������, �������� � ������
		/// </summary>
		[TestMethod]
		public void CannotReadVelocity()
		{
			var movableObject = new Mock<IMovable>();

			movableObject.SetupProperty(x => x.Position, new Vector(new int[] { 12, 5 }));
			movableObject.SetupGet(x => x.Velocity).Throws<Exception>();

			var move = new Move(movableObject.Object);

			Assert.ThrowsException<Exception>(() => move.Execute(), "������� �������� ������, � �������� ���������� ��������� �������� ���������� ��������, �������!");
		}


		/// <summary>
		/// ������� �������� ������, � �������� ���������� �������� ��������� � ������������, �������� � �������
		/// </summary>
		[TestMethod]
		public void CannotChangePosition()
		{
			var movableObject = new Mock<IMovable>();

			movableObject.SetupProperty(x => x.Position, new Vector(new int[] { 12, 5 }));
			movableObject.SetupGet(x => x.Velocity).Returns(() => new Vector(new int[] { -7, 3 }));

			movableObject.SetupSet(x => x.Position = It.IsAny<Vector>()).Throws<Exception>();

			var move = new Move(movableObject.Object);

			Assert.ThrowsException<Exception>(() => move.Execute(), "������� �������� ������, � �������� ���������� �������� ��������� � ������������, �������!");
		}
	}
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpaceBattle.Repository;
using SpaceBattle.Repository.Commands;

namespace SpaceBattle.Tests
{
	[TestClass]
	public class ChangeVelocityComamndTest
	{
		/// <summary>
		/// При повороте должен изменится вектор мгновеной скорости
		/// </summary>
		[TestMethod]
		public void CheckChangeVelocity()
		{
			var changeVelocityObject = new Mock<IChangeVelocity>();

			changeVelocityObject.SetupProperty(x => x.Velocity, new Repository.Vector(new int[] { 1, 1 }));
			changeVelocityObject.SetupProperty(x => x.Direction, 1);
			changeVelocityObject.SetupGet(x => x.DirectionsNumber).Returns(8);

			var checkFuel = new ChangeVelocityCommand(changeVelocityObject.Object);
			checkFuel.Execute();

			Assert.IsTrue(changeVelocityObject.Object.Velocity != new Repository.Vector(new int[] { 1, 1 }), "При повороте вектор мгновенной скорости не изменился!");
		}
	}
}

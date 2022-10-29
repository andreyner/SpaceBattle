using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpaceBattle.Repository.Commands;
using SpaceBattle.Repository.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Tests
{
	[TestClass]
	public class CheckFuelComamndTest
	{
		/// <summary>
		/// Если топливо есть, например 1ед, то ошибки быть не должно
		/// </summary>
		[TestMethod]
		public void FuelIsMoreThenZero()
		{
			var checkFuelObject = new Mock<ICheckFuel>();

			checkFuelObject.SetupGet(x => x.FuelVolume).Returns(1);

			var checkFuel = new CheckFuel(checkFuelObject.Object);
			checkFuel.Execute();

			Assert.IsTrue(true);
		}

		/// <summary>
		/// Если топлива нет, то должна быть ошибка
		/// </summary>
		[TestMethod]
		public void FuelIsEqualZero()
		{
			var checkFuelObject = new Mock<ICheckFuel>();

			checkFuelObject.SetupGet(x => x.FuelVolume).Returns(0);

			var checkFuel = new CheckFuel(checkFuelObject.Object);
			
			Assert.ThrowsException<CommandException>(() => checkFuel.Execute(), "При проверке отсутствия топлива ошибки не произошло");
		}
	}
}

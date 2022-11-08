using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpaceBattle.Repository.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Tests
{
	[TestClass]
	public class BurnFuelComamndTest
	{
		/// <summary>
		/// При сжигании 1 ед топлива из 5ед, должно остаться 4ед
		/// </summary>
		[TestMethod]
		public void FuelIsMoreThenZero()
		{
			var burnFuelObject = new Mock<IBurnFuel>();

			burnFuelObject.SetupProperty(x => x.FuelVolume, 5);
			burnFuelObject.SetupGet(x => x.FuelExpense).Returns(1);

			var checkFuel = new BurnFuel(burnFuelObject.Object);
			checkFuel.Execute();

			burnFuelObject.VerifySet(x => x.FuelVolume = It.Is<int>(x => x == 4), "Неверный результат сжигания топлива!");

			Assert.IsTrue(true);
		}
	}
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceBattle.Repository;
using SpaceBattle.Repository.Collision;
using SpaceBattle.Repository.Commands;
using SpaceBattle.Repository.Container;
using SpaceBattle.Repository.GameObjects;

namespace SpaceBattle.Tests
{
	[TestClass]
	public class CollisionTest
	{
		[TestInitialize]
		public void TestInitialize()
		{
			new InitScopesCommand().Execute();
		}

		[TestMethod]
		public void SectorCheckCommandSuccsess()
		{
			var mainSectors = Sector.InitializeSectors();

			var ship1 = new SpaceShip();
			ship1["id"] = "ship1";
			ship1["Positiion"] = new Vector(new int[] { 1, 5 });

			var ship2 = new SpaceShip();
			ship2["id"] = "ship2";
			ship2["Positiion"] = new Vector(new int[] { 1, 9 });

			var ship3 = new SpaceShip();
			ship3["id"] = "ship3";
			ship3["Positiion"] = new Vector(new int[] { 1, 35 });

			mainSectors[0].Spaceships.Add(ship3);
			mainSectors[3].Spaceships.Add(ship3);

			var sectorCheckCommand = IoC.Resolve<SectorCheckCommand>("SectorCheckCommand", ship1, mainSectors[0]);
			sectorCheckCommand.Execute();

			Assert.IsTrue(true);
		}
	}
}

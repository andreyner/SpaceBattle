using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SpaceBattle.Repository.Adapters;
using System.Collections.Generic;
using System.Text;
using SpaceBattle.Repository;
using Moq;
using SpaceBattle.Repository.Commands;

namespace SpaceBattle.Tests
{
	[TestClass]
	public class AdapterGenerateTest
	{
		[TestInitialize]
		public void TestInitialize()
		{
			new InitScopesCommand().Execute();
		}

		[TestMethod]
		public void TryCreateAdapter()
		{
			var gameObject = new Mock<Uobject>();
			new MoveCommandPluginCommand().Execute();

			var adapter = CodeGenerator.CreateAdapter(typeof(IMovable), gameObject.Object);

			((IMovable)adapter).Position = new Vector(new int[] { -7, 3 });

			Assert.IsNotNull(adapter);
		}
	}
}

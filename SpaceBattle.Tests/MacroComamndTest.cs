using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpaceBattle.Repository;
using SpaceBattle.Repository.Exception;
using SpaceBattle.Repository.MacroCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Tests
{
	[TestClass]
	public class MacroComamndTest
	{
		/// <summary>
		/// При выполнении цепочки команд, дожно вызываться исплнении каждой команды в цепочке
		/// </summary>
		[TestMethod]
		public void ExecuteAllCommands()
		{
			var correctCommand1Object = new Mock<ICommand>();
			var correctCommand2Object = new Mock<ICommand>();
			var correctCommand3Object = new Mock<ICommand>();

			var macroCmd = new MacroComamnd(new ICommand[] { correctCommand1Object.Object, correctCommand2Object.Object, correctCommand3Object.Object });
			macroCmd.Execute();

			correctCommand1Object.Verify(m => m.Execute(), Times.Once);
			correctCommand2Object.Verify(m => m.Execute(), Times.Once);
			correctCommand3Object.Verify(m => m.Execute(), Times.Once);
		}

		/// <summary>
		/// В случае появления ошибки, цепочка выполнения комманд должна прерваться
		/// </summary>
		[TestMethod]
		public void AbortCommandIfThrowExeption()
		{
			var correctCommand1Object = new Mock<ICommand>();
			var errorCommandObject = new Mock<ICommand>();
			var correctCommand2Object = new Mock<ICommand>();

			errorCommandObject.Setup(x => x.Execute()).Throws(new CommandException());

			var macroCmd = new MacroComamnd(new ICommand[] { correctCommand1Object.Object, errorCommandObject.Object, correctCommand2Object.Object});
			try
			{
				macroCmd.Execute();
			}
			catch (CommandException)
			{ }

			correctCommand2Object.Verify(m => m.Execute(), Times.Never);
		}
	}
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpaceBattle.Repository;
using SpaceBattle.Repository.Commands;
using SpaceBattle.Repository.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Tests
{
	[TestClass]
	public class ExceptionHandlerTest
	{
		/// <summary>
		///Тест команды, которая записывает информацию о выброшенном исключении в лог.
		/// </summary>
		[TestMethod]
		public void CommandLogWrite()
		{
			var testCmd = new Mock<ICommand>();

			var queue = new Mock<Queue<ICommand>>(new List<ICommand> { testCmd.Object });
			var strategy = new ExceptionHandler(new OnlyLog());

			var queueExecuter = new Mock<QueueExecuter>(strategy, queue.Object);

			testCmd.Setup(x => x.Execute()).Throws<Exception>();
			queueExecuter.Object.Execute();
		}

		/// <summary>
		///Тест обработчика исключения, который ставит в очередь Команду - повторитель команды, выбросившей исключение.
		/// </summary>
		[TestMethod]
		public void CommandRepeat()
		{
			var testCmd = new Mock<ICommand>();

			var queue = new Mock<Queue<ICommand>>(new List<ICommand> { testCmd.Object });
			var strategy = new ExceptionHandler(new RepeatCommand());

			var queueExecuter = new Mock<QueueExecuter>(strategy, queue.Object);

			testCmd.Setup(x => x.Execute()).Throws<Exception>();
			queueExecuter.Object.Execute();
		}

		/// <summary>
		///Тест обработчика исключения: при первом выбросе исключения повторить команду, при повторном выбросе исключения записать информацию в лог.
		/// </summary>
		[TestMethod]
		public void CommandRepeatAndLog()
		{
			var testCmd = new Mock<ICommand>();

			var queue = new Mock<Queue<ICommand>>(new List<ICommand> { testCmd.Object });
			var strategy = new ExceptionHandler(new OneRepeatAndLogCommand());

			var queueExecuter = new Mock<QueueExecuter>(strategy, queue.Object);

			testCmd.Setup(x => x.Execute()).Throws<Exception>();
			queueExecuter.Object.Execute();
		}

		/// <summary>
		///Тест Обработчика исключения: повторить два раза, потом записать в лог.
		/// </summary>
		[TestMethod]
		public void CommandTwoRepeatAndLog()
		{
			var testCmd = new Mock<ICommand>();

			var queue = new Mock<Queue<ICommand>>(new List<ICommand> { testCmd.Object });
			var strategy = new ExceptionHandler(new TwoRepeatAndLogCommand());

			var queueExecuter = new Mock<QueueExecuter>(strategy, queue.Object);

			testCmd.Setup(x => x.Execute()).Throws<Exception>();
			queueExecuter.Object.Execute();
		}
	}
}

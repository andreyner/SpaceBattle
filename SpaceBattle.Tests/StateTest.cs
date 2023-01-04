using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceBattle.Repository.Commands;
using SpaceBattle.Repository.Container;
using SpaceBattle.Repository.EventLoop;
using SpaceBattle.Repository.State;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SpaceBattle.Repository.State.CommandState;

namespace SpaceBattle.Tests
{
	[TestClass]
	public class StateTest
	{
		[TestInitialize]
		public void TestInitialize()
		{
			new InitScopesCommand().Execute();
		}

		/// <summary>
		/// Написать тест, который проверяет, что после команды MoveToCommand, поток переходит на обработку Команд с помощью состояния MoveTo
		/// </summary>
		[TestMethod]
		public void TryExecuteMoveToCommand()
		{
			var eventLoop = new EventLoop();

			Assert.IsTrue(eventLoop.commandState.GetType() != typeof(MoveToState));

			for (int i = 0; i < 10; i++)
			{
				eventLoop.EnqueueWithoutAwakening(new EmptyCommand());
			}

			eventLoop.EnqueueWithoutAwakening(new MoveToCommand());
			var task = Task.Run(() => eventLoop.Start());
			task.Wait(new TimeSpan(0, 0, 5));

			Assert.IsTrue(eventLoop.commandState.currentState.GetType() == typeof(MoveToState));
		}

		/// <summary>
		/// Написать тест, который проверяет, что после команды RunCommand, поток переходит на обработку Команд с помощью состояния "Обычное" 
		/// </summary>
		[TestMethod]
		public void TryExecuteRunCommand()
		{
			var eventLoop = new EventLoop();

			for (int i = 0; i < 10; i++)
			{
				eventLoop.EnqueueWithoutAwakening(new EmptyCommand());
			}
			eventLoop.EnqueueWithoutAwakening(new MoveToCommand());
			eventLoop.EnqueueWithoutAwakening(new RunCommand());

			var task = Task.Run(() => eventLoop.Start());
			task.Wait(new TimeSpan(0, 0, 5));

			Assert.IsTrue(eventLoop.commandState.currentState.GetType() == typeof(DefaultState));
		}

		/// <summary>
		/// Написать тест, который проверяет, что после команды hard stop, поток завершается
		/// </summary>
		[TestMethod]
		public void TryExecuteHardStopCommand()
		{
			var eventLoop = new EventLoop();

			for (int i = 0; i < 10; i++)
			{
				eventLoop.EnqueueWithoutAwakening(new EmptyCommand());
			}

			eventLoop.EnqueueWithoutAwakening(new HardStopCommand(eventLoop));


			for (int i = 0; i < 10; i++)
			{
				eventLoop.EnqueueWithoutAwakening(new EmptyCommand());
			}

			eventLoop.Start();

			Assert.IsTrue(eventLoop.ActionQueue.Count == 10);
		}
	}
}

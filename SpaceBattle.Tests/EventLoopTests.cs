using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceBattle.Repository.Commands;
using SpaceBattle.Repository.Container;
using SpaceBattle.Repository.EventLoop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceBattle.Tests
{
	[TestClass]
	public class EventLoopTests
	{
		[TestInitialize]
		public void TestInitialize()
		{
			new InitScopesCommand().Execute();
		}

		[TestMethod]
		public void TryHardStopEventLoop()
		{
			var thread = IoC.Resolve<(EventLoop, Thread)>("Thread.Start");

			for (int i = 0; i < 10; i++)
			{
				thread.Item1.EnqueueWithoutAwakening(new EmptyCommand());
			}

			thread.Item1.EnqueueWithoutAwakening(new HardStopCommand(thread.Item1));

			for (int i = 0; i < 100; i++)
			{
				thread.Item1.EnqueueWithoutAwakening(new EmptyCommand());

			}

			thread.Item1.Start();
			thread.Item2.Join();

			Assert.IsTrue(true);
		}


		[TestMethod]
		public void TrySoftStopEventLoop()
		{
			var thread = IoC.Resolve<(EventLoop, Thread)>("Thread.Start");

			for (int i = 0; i < 10; i++)
			{
				thread.Item1.EnqueueWithoutAwakening(new EmptyCommand());
			}

			thread.Item1.EnqueueWithoutAwakening(new SoftStopCommand(thread.Item1));

			for (int i = 0; i < 100; i++)
			{
				thread.Item1.EnqueueWithoutAwakening(new EmptyCommand());

			}

			thread.Item1.Start();
			thread.Item2.Join();

			Assert.IsTrue(thread.Item1.ActionQueue.Count == 0);
		}
	}
}

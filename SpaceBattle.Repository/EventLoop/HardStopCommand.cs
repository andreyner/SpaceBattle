using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceBattle.Repository.EventLoop
{
	public class HardStopCommand : ICommand
	{
		private readonly EventLoop eventLoop;

		public HardStopCommand(EventLoop eventLoop)
		{
			this.eventLoop = eventLoop;
		}

		public void Execute()
		{
			eventLoop.HardStop();
		}
	}
}

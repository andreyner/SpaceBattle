using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.EventLoop
{
	public class SoftStopCommand : ICommand
	{
		private readonly EventLoop eventLoop;
		public SoftStopCommand(EventLoop eventLoop)
		{
			this.eventLoop = eventLoop;
		}

		public void Execute()
		{
			eventLoop.SoftStop();
		}
	}
}

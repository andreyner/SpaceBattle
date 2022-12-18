using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceBattle.Repository.EventLoop
{
	public class EventLoopStartCommand : ICommand
	{
		public void Execute()
		{
			var eventLoop = new EventLoop();
			var task = new Thread(() => eventLoop.Start());
			task.Start();
		}
	}

}

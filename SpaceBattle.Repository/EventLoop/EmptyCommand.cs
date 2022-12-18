using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceBattle.Repository.EventLoop
{
	public class EmptyCommand : ICommand
	{
		public void Execute()
		{
			Thread.Sleep(10);
		}
	}
}

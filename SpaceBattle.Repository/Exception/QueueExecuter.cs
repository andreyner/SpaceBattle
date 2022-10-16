using System;
using System.Collections.Generic;
using System.Text;
using SpaceBattle.Repository.Exception;

namespace SpaceBattle.Repository.Exception
{
	public class QueueExecuter : ICommand
	{
		private readonly IExceptionHandler _exceptionHandler;
		private readonly Queue<ICommand> _queue;

		public QueueExecuter(ExceptionHandler exceptionHandler, Queue<ICommand> queue)
		{
			_exceptionHandler = exceptionHandler;
			_queue = queue;
		}

		public void Execute()
		{

			ICommand currentCmd = null;

			while (true)
			{
				try
				{
					currentCmd = _queue.Dequeue();
					currentCmd.Execute();
				}
				catch (System.Exception ex)
				{
					_exceptionHandler.Handle(currentCmd, ex, _queue);
					if (_queue.Count == 0) { break; }
				}
			}
		}
	}
}

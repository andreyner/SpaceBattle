using SpaceBattle.Repository.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SpaceBattle.Repository.Exception
{
	public interface IExceptionHandler
	{
	   void Handle(ICommand cmd, System.Exception ex, Queue<ICommand> queue);
	}

	public interface IExceptionStrategy
	{
		Dictionary<string, Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>> GetChainHandle();
	}


	public class ExceptionHandler : IExceptionHandler
	{
		private Dictionary<string, Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>> _handles;

		public ExceptionHandler(IExceptionStrategy exceptionStrategy)
		{
			_handles = exceptionStrategy.GetChainHandle();
		}

		public void Handle(ICommand cmd, System.Exception ex, Queue<ICommand> queue)
		{
			if (!_handles.TryGetValue(cmd.GetType().FullName, out Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>  exceptionHandles)) 
			{
				exceptionHandles = _handles[_handles.Keys.First()];
			}

			if (exceptionHandles.TryGetValue(ex.GetType().Name, out Action<ICommand, System.Exception, Queue<ICommand>> func))
			{
				func(cmd, ex, queue);
			}

		}
	}

	/// <summary>
	/// Обработчик исключения, который ставит Команду, пишущую в лог в очередь Команд
	/// </summary>
	public class OnlyLog : IExceptionStrategy
	{
		private Dictionary<string, Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>> _handles;

		public OnlyLog()
		{
			_handles = new Dictionary<string, Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>>();
			_handles.Add(typeof(ICommand).FullName, new Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>> { { typeof(System.Exception).Name, (cmd, ex, queue) => queue.Enqueue(new Log(cmd, ex)) } });

		}

		public Dictionary<string, Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>> GetChainHandle()
		{
			return _handles;
		}
	}

	/// <summary>
	/// Обработчик исключения, который ставит в очередь Команду - повторитель команды, выбросившей исключение.
	/// </summary>
	public class RepeatCommand : IExceptionStrategy
	{
		private Dictionary<string, Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>> _handles;

		public RepeatCommand()
		{
			_handles = new Dictionary<string, Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>>();
			_handles.Add(typeof(ICommand).FullName, new Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>> { { typeof(System.Exception).Name, (cmd, ex, queue) => queue.Enqueue(new OneRepeatCommand(cmd)) } });
			_handles.Add(typeof(OneRepeatCommand).FullName, new Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>> { { typeof(System.Exception).Name, (cmd, ex, queue) => { } } });
		}

		public Dictionary<string, Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>> GetChainHandle()
		{
			return _handles;
		}
	}

	/// <summary>
	/// Обработчик исключения: при первом выбросе исключения повторить команду, при повторном выбросе исключения записать информацию в лог.
	/// </summary>
	public class OneRepeatAndLogCommand : IExceptionStrategy
	{
		private Dictionary<string, Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>> _handles;

		public OneRepeatAndLogCommand()
		{
			_handles = new Dictionary<string, Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>>();
			_handles.Add(typeof(ICommand).FullName, new Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>> { { typeof(System.Exception).Name, (cmd, ex, queue) => queue.Enqueue(new OneRepeatCommand(cmd)) } });
			_handles.Add(typeof(OneRepeatCommand).FullName, new Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>> { { typeof(System.Exception).Name, (cmd, ex, queue) => queue.Enqueue(new Log(cmd, ex)) } });
		}

		public Dictionary<string, Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>> GetChainHandle()
		{
			return _handles;
		}
	}

	/// <summary>
	/// Обработчик исключения: повторить два раза, потом записать в лог.
	/// </summary>
	public class TwoRepeatAndLogCommand : IExceptionStrategy
	{
		private Dictionary<string, Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>> _handles;

		public TwoRepeatAndLogCommand()
		{
			_handles = new Dictionary<string, Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>>();
			_handles.Add(typeof(ICommand).FullName, new Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>> { { typeof(System.Exception).Name, (cmd, ex, queue) => queue.Enqueue(new OneRepeatCommand(cmd)) } });
			_handles.Add(typeof(OneRepeatCommand).FullName, new Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>> { { typeof(System.Exception).Name, (cmd, ex, queue) => queue.Enqueue(new TwoRepeatCommand(cmd)) } });
			_handles.Add(typeof(TwoRepeatCommand).FullName, new Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>> { { typeof(System.Exception).Name, (cmd, ex, queue) => queue.Enqueue(new Log(cmd, ex)) } });
		}

		public Dictionary<string, Dictionary<string, Action<ICommand, System.Exception, Queue<ICommand>>>> GetChainHandle()
		{
			return _handles;
		}
	}
}

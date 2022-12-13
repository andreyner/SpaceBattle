using SpaceBattle.Model;
using SpaceBattle.Repository.Container;
using System.Collections.Concurrent;


namespace SpaceBattle.Repository.RabbitMq
{
	public class InetrpretCommand : ICommand
	{
		private readonly CommandMessage commandMessage;

		public InetrpretCommand(CommandMessage commandMessage)
		{
			this.commandMessage = commandMessage;
		}

		public void Execute()
		{
			var cmd = IoC.Resolve<ICommand>(commandMessage.OperationKey, commandMessage.args);

			var currentQueue  = IoC.Resolve<ConcurrentQueue<ICommand>>("QueueCommand.Get");

			currentQueue.Enqueue(cmd);
		}
	}
}

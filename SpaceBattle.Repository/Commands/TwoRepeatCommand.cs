using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Commands
{
	/// <summary>
	/// Команда, которая повторяет Команду, выбросившую исключение.
	/// </summary>
	public class TwoRepeatCommand : ICommand
	{
		private readonly ICommand sourceCommand;

		public TwoRepeatCommand(ICommand sourceCommand)
		{
			this.sourceCommand = sourceCommand;
		}

		public void Execute()
		{
			sourceCommand.Execute();
		}
	}
}

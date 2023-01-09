using SpaceBattle.Repository.Exception;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.MacroCommands
{
	public class MacroComamnd: ICommand
	{
		private readonly ICommand[] _commands;
		private List<ICommand> commands;

		public MacroComamnd(ICommand [] commands)
		{
			_commands = commands;
		}

		public MacroComamnd(List<ICommand> commands)
		{
			this.commands = commands;
		}

		public void Execute()
		{
			foreach (var cmd in _commands)
			{
				try
				{
					cmd.Execute();
				}
				catch(System.Exception ex)
				{
					throw new CommandException(ex.Message, ex);
				}
			}
		}
	}
}

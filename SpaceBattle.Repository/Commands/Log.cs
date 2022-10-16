using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Commands
{
	/// <summary>
	/// Команда, которая записывает информацию о выброшенном исключении в лог
	/// </summary>
	public class Log : ICommand
	{
		private readonly ICommand cmd;

		private readonly System.Exception ex;

		public Log(ICommand cmd, System.Exception ex)
		{
			this.cmd = cmd;
			this.ex = ex;
		}

		public void Execute()
		{
			Console.WriteLine($"Произошла ошибка в приложении {ex.Message}");
		}
	}
}

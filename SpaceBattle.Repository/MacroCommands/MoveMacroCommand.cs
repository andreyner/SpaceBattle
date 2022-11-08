using SpaceBattle.Repository.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.MacroCommands
{
	/// <summary>
	/// Реализовать команду движения по прямой с расходом топлива
	/// </summary>
	public class MoveMacroCommand : ICommand
	{
		private readonly CheckFuel _checkFuel;
		private readonly Move _move;
		private readonly BurnFuel _burnFuel;

		public MoveMacroCommand(CheckFuel checkFuel, Move move, BurnFuel burnFuel)
		{
			_checkFuel = checkFuel;
			_move = move;
			_burnFuel = burnFuel;
		}

		public void Execute()
		{
			new MacroComamnd(new ICommand[] { _checkFuel, _move, _burnFuel }).Execute();
		}
	}
}

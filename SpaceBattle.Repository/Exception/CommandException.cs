using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Exception
{
	public class CommandException : System.Exception
	{
		public CommandException()
		{
		}

		public CommandException(string message) : base(message)
		{
		}

		public CommandException(string message, System.Exception innerException) : base(message, innerException)
		{
		}
	}
}

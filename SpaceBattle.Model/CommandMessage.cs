using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Model
{
	public class CommandMessage
	{
		public string GameKey { get; set; }

		public Guid GameObjKey { get; set; }

		public string OperationKey { get; set; }

		public object [] args { get; set; }
		
	}
}

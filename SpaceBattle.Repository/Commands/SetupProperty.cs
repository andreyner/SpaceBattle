using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Commands
{
	public class SetupPropertyCommand : ICommand
	{
		private Uobject obj;
		private string key;
		private object value;

		public SetupPropertyCommand(Uobject obj, string key, object value)
		{
			this.obj = obj;
			this.key = key;
			this.value = value;
		}

		public void Execute()
		{
			obj[key] = value;
		}
	}
}

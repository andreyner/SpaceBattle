using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Container
{
	public class SetCurrentScopeCommand : ICommand
	{
		private readonly string scopeId;

		public SetCurrentScopeCommand(string scopeId)
		{
			this.scopeId = scopeId;
		}

		public void Execute()
		{
			ScopeRepository.Value.CurrentScope.Value.Id = scopeId;
		}
	}
}

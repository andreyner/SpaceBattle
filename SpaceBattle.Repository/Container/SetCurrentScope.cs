using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Container
{
	public class SetCurrentScopeCommand : ICommand
	{
		private readonly Scope scope;

		public SetCurrentScopeCommand(Scope scope)
		{
			this.scope = scope;
		}

		public void Execute()
		{
			ScopeBaseDependencyStrategy.CurrentScope.Value = scope;
		}
	}
}

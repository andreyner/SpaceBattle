using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Container
{
	public class RegisterBaseCommand : ICommand
	{
		private readonly string scopeId;

		public RegisterBaseCommand(string scopeId)
		{
			this.scopeId = scopeId;
		}

		public void Execute()
		{
			new CreateScopeCommand(scopeId).Execute();
			new SetCurrentScopeCommand(scopeId).Execute();

			new IoCRegisterCommand("IoC.Register", "Scopes.New", (Func<object[], object>)((args) => { return new CreateScopeCommand((string)args[0]); })).Execute();
			new IoCRegisterCommand("IoC.Register", "Scopes.Current", (Func<object[], object>)((args) => { return new SetCurrentScopeCommand((string)args[0]); })).Execute();

			new IoCRegisterCommand("IoC.Register", "IoC.Register", (Func<object[], object>)((args) => { return new IoCRegisterCommand("IoC.Register", args); })).Execute();
		}
	}
}

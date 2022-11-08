using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Container
{
	public class IoCRegisterCommand : ICommand
	{
		private readonly string key;
		private readonly object[] args;

		public IoCRegisterCommand(string key, params object[] args)
		{
			this.key = key;
			this.args = args;
		}

		public void Execute()
		{
			var scopeId = ScopeRepository.Value.CurrentScope.Value.Id;

			if(ScopeRepository.Value.repository.TryGetValue(scopeId, out var impl))
			{
				impl.Add((string)args[0], (Func<object[], object>)args[1]);
			}
		}
	}
}

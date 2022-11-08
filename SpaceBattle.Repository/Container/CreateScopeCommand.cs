using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Container
{
	public class CreateScopeCommand : ICommand
	{
		private readonly string scopeId;

		public CreateScopeCommand(string scopeId)
		{
			this.scopeId = scopeId;
		}

		public void Execute()
		{
			if (ScopeRepository.Value.repository.ContainsKey(scopeId))
			{
				throw new System.Exception($"Скоуп уже добавлен!");
			}
			ScopeRepository.Value.repository.TryAdd(scopeId, new Dictionary<string, Func<object[], object>>());

		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Container
{
	public class IoC : IContainer
	{

		public T Resolve<T>(string key, params object[] args)
		{
			if(ScopeRepository.Value.repository.TryGetValue(ScopeRepository.Value.CurrentScope.Value.Id, out var store)) 
			{		
				if(store.TryGetValue(key, out var impl))
				{
					return (T)impl(args);
				}
				else
				{
					throw new System.Exception($"Не удалось найти реализацию {key}!");
				}
			}
			else
			{
				throw new System.Exception("Не удалось найти скоуп!");
			}

		}
	}
}

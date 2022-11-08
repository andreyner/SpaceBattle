using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SpaceBattle.Repository.Container
{
	public class ScopeRepository
	{
		private static readonly ScopeRepository instance = new ScopeRepository();

		private ScopeRepository()
		{
			/*var scope = new Scope();
			CurrentScope = new ThreadLocal<IScope>(() =>
			{
				return scope;
			});*/
			repository = new ConcurrentDictionary<string, Dictionary<string, Func<object[], object>>>();
		}

		public ConcurrentDictionary<string, Dictionary<string, Func<object[], object>>> repository;

		public static ScopeRepository Value { get => instance; }

		public ThreadLocal<IScope> CurrentScope  = new ThreadLocal<IScope>(() =>
			{
				return new Scope();
			});
	}
}

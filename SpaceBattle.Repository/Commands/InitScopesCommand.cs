using SpaceBattle.Repository.Adapters;
using SpaceBattle.Repository.Container;
using SpaceBattle.Repository.EventLoop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpaceBattle.Repository.Commands
{
	public class InitScopesCommand : ICommand
	{
		public void Execute()
		{
			if(ScopeBaseDependencyStrategy.Root!= null)
			{
				return;
			}
			Func<string, object[], object> str = ScopeBaseDependencyStrategy.Resolve;

			IoC.Resolve<ICommand>("IoC.SetupStrategy", str).Execute();

			var dependencies = new ConcurrentDictionary<string, Func<object[], object>>();

			var scope = new Scope(
				dependencies,
				null
				);

			dependencies.TryAdd("Scopes.Storage", args =>
			{
				return new ConcurrentDictionary<string, Func<object[], object>>();
			});

			dependencies.TryAdd("Scopes.New", (args) =>
			{
				return new Scope(IoC.Resolve<IDictionary<string,
					Func<object[], object>>>("Scopes.Storage"),
					(Scope)args[0]);
			});

			dependencies.TryAdd("Scopes.Current", args =>
			{
				var scope = ScopeBaseDependencyStrategy.CurrentScope.Value;
				if (scope != null)
				{
					return scope;
				}
				else
				{
					return ScopeBaseDependencyStrategy.DefaultScope;
				}
			});

			dependencies.TryAdd("Scopes.Current.Set", args =>
			{
				return new SetCurrentScopeCommand((Scope)args[0]);
			});

			dependencies.TryAdd("IoC.Register", args =>
			{
				return new IoCRegisterCommand((string)args[0], (Func<object[], object>)args[1]);
			});

			dependencies.TryAdd("SetupProperty", args =>
			{
				return new SetupPropertyCommand((Uobject)args[0], (string)args[1], args[2]);
			});

			dependencies.TryAdd("Adapter", args =>
			{
				return CodeGenerator.CreateAdapter((Type)args[0], args[1]);
			});

			dependencies.TryAdd("Thread.Start", args =>
			{
				var eventLoop = new EventLoop.EventLoop();
				var task = new Thread(() => eventLoop.Start());
				task.Start();
				return (eventLoop: eventLoop, task: task);
			});

			ScopeBaseDependencyStrategy.Root = scope;

			new SetCurrentScopeCommand(scope).Execute();
		}
	}
}

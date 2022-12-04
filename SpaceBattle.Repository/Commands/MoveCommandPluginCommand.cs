using SpaceBattle.Repository.Container;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Commands
{
	public class MoveCommandPluginCommand : ICommand
	{
		public void Execute()
		{
			IoC.Resolve<ICommand>(
				"IoC.Register",
				"Movable.Position.Get",
				(Func<object[], object>)((args) => ((Uobject)args[0])["Position"])
				).Execute();

			IoC.Resolve<ICommand>(
				"IoC.Register",
				"Movable.Position.Set",
				(Func<object[], object>)((args) => {
					IoC.Resolve<ICommand>("SetupProperty", args[0], args[1], args[2]).Execute();
					return args[2];
				})
				).Execute();

			IoC.Resolve<ICommand>(
				"IoC.Register",
				"Movable.Velocity.Get",
				(Func<object[], object>)((args) => ((Uobject)args[0])["Position"])
				).Execute();

		}


	}
}

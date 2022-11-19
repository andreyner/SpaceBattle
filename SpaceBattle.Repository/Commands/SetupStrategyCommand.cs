﻿using SpaceBattle.Repository.Container;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Commands
{
	public class SetupStrategyCommand : ICommand
	{
		private readonly Func<string, object[], object> newStrategy;

		public SetupStrategyCommand(Func<string, object[], object> newStrategy) => this.newStrategy = newStrategy;
		public void Execute()
		{
			IoC.Strategy = this.newStrategy;
		}
	}
}

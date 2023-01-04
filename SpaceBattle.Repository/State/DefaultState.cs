using SpaceBattle.Repository.Commands;
using SpaceBattle.Repository.Container;
using SpaceBattle.Repository.EventLoop;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBattle.Repository.State
{
	public partial class CommandState
	{
		public ICommandExecutorState currentState;

		public CommandState()
		{
			this.currentState = new DefaultState(this);
		}

		public void Execute(ICommand action)
		{
			currentState.Handle(action);
		}

		public class DefaultState : ICommandExecutorState
		{

			private CommandState state;

			public DefaultState(CommandState currentState)
			{
				this.state = currentState;
			}

			public void Handle(ICommand action)
			{
				switch (action)
				{
					case HardStopCommand hardStop: 
						state.currentState = null;
						action.Execute();
						break;
					case MoveToCommand moveToCommand:
						state.currentState = new MoveToState(state);
						state.currentState.Handle(action);
						break;
					default: state.currentState = this;
						action.Execute();
						break;
				}
			}
		}
	}
}

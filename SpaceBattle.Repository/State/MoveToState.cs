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
	partial class CommandState
	{
		public class MoveToState : ICommandExecutorState
		{
			private CommandState state;

			public MoveToState(CommandState currentState)
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
					case RunCommand runCommand:
						state.currentState = new DefaultState(state);
						state.currentState.Handle(action);
						break;
					default:
						state.currentState = this;
						action.Execute();
						break;
				}
			}
		}
	}
}

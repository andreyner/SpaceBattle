using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.State
{
	public interface ICommandExecutorState
	{
		void Handle(ICommand command);
	}
}

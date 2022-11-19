using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Container
{
	public interface IContainer
	{
		T Resolve<T>(string key, params object[] args);
	}
}

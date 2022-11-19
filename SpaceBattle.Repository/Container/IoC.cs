using SpaceBattle.Repository.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.Container
{
	public class IoC
	{
		internal static Func<string, object[], object> Strategy { get; set; } = DefaultStrategy;

		public static T Resolve<T>(string key, params object[] args)
		{
			return (T)Strategy(key, args);
		}

		private static object DefaultStrategy(string key, object[] args) 
		{ 
			if("IoC.SetupStrategy" == key)
			{
				return new SetupStrategyCommand((Func<string, object[], object>)args[0]);
			}
			else
			{
				throw new ArgumentException("Unknown IoC dependency");
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository
{
	public abstract class Uobject
	{
		public abstract object this[string key]
		{
			get;
			set;
		}
	}
}

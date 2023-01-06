using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository
{
	public abstract class Uobject
	{
		protected Dictionary<string, object> values;

		protected Uobject()
		{
			values = new Dictionary<string, object>();
		}

		public abstract object this[string key]
		{
			get;
			set;
		}
	}
}

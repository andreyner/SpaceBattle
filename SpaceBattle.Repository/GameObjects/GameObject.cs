using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.GameObjects
{
	public class GameObject: Uobject
	{
		public override object this[string key]
		{
			get
			{
				return values[key];
			}

			set
			{
				values.Add(key, value);
			}
		}

		public object GetProperty(string key)
		{
			return values[key];
		}

		public Dictionary<string, object> GetProperties()
		{
			return values;
		}

		public void SetProperty(string key, object value)
		{
			values.Add(key, value);
		}
	}
}

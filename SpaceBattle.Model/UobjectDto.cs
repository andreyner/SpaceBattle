using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Model
{
	public class UobjectDto
	{
		public Dictionary<string, object> values { get; set; }

		public object GetProperty(string key)
		{
			return values[key];
		}

		public Dictionary<string, object> GetProperties()
		{
			return values;
		}
	}
}

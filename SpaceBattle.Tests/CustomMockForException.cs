using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Tests
{
	public class CustomMockForException<T> : Mock<T>
		where T : class
	{
		public override T Object => base.Object;
	}
}

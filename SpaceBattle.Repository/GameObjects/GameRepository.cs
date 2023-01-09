using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.GameObjects
{
	public class GameRepository
	{
		public List<Game> Games { get; set; }

		public GameRepository()
		{
			Games = new List<Game>();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceBattle.Repository.GameObjects
{
	public class Game
	{
		public List<GameObject> GameObjects
		{
			get; private set;
		}
		public string Id { get; }

		public Game(string id)
        {

            Id = id;

            var spaceship_1 = new GameObject();
            spaceship_1.SetProperty("userName", "Евгений");
            spaceship_1.SetProperty("id", "35E80DBE-78F7-41CC-8183-AA3359FDCF2E");
            spaceship_1.SetProperty("fuel", (decimal)5);

            GameObjects.Add(spaceship_1);

            var spaceship_2 = new GameObject();
            spaceship_2.SetProperty("userName", "Олег");
            spaceship_2.SetProperty("id", "35E80DBE-78F7-41CC-8184-AA3359FDCF2E");
            spaceship_2.SetProperty("fuel", (decimal)10);
            GameObjects.Add(spaceship_2);

            var spaceship_3 = new GameObject();
            spaceship_3.SetProperty("userName", "Владимир");
            spaceship_3.SetProperty("id", "35E80DBE-78F7-41CC-8283-AA3359FDCF2E");
            spaceship_3.SetProperty("fuel", (decimal)150);
            GameObjects.Add(spaceship_3);
        }
    }
}

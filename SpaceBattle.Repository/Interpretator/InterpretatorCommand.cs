using SpaceBattle.Model;
using SpaceBattle.Repository.Container;
using SpaceBattle.Repository.GameObjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SpaceBattle.Repository.Interpretator
{
	public class InterpretatorCommand : ICommand
	{
		UobjectDto gameCommand;

		public InterpretatorCommand(UobjectDto gameCommand)
		{
			this.gameCommand = gameCommand;
		}

		public void Execute()
		{
			var gameObjectId = Guid.Parse(gameCommand.GetProperty("GameObjKey").ToString());
			var gameKey = (string)gameCommand.GetProperty("GameKey");
			var action = (string)gameCommand.GetProperty("OperationKey");
			var args = gameCommand.values.ContainsKey("Args")?(Dictionary<string, object>)gameCommand.GetProperty("Args"): new Dictionary<string, object>();

			Type t = Type.GetType($"{action}");
			if (t == null)
			{
				throw new System.Exception($"Тип {action} не найден");
			}

			var constructors = t.GetConstructors();

			if(constructors.Length > 0)
			{
				var constructor = constructors[0];

				var cmdProp = gameCommand.GetProperties();

				var prms = new List<object>();

				var gameRep = IoC.Resolve<GameRepository>("GameRepository.Get");

				var game = gameRep.Games.Single(x => x.Id == gameKey);

				var gameObject = game.GameObjects.Single(x => (Guid)x.GetProperty("id") == gameObjectId);

				foreach (var constructorParam in constructor.GetParameters())
				{
					//Поиск среди основных параметров
					if (cmdProp.TryGetValue(constructorParam.Name, out var paramValue)) {

						prms.Add(paramValue);
					}
					else
					{
						//поиск среди дополнительных параметров
						if (args.TryGetValue(constructorParam.Name, out paramValue))
						{
							prms.Add(paramValue);
						}
						else
						{
							if (!constructorParam.IsOptional)
							{
								throw new System.Exception($"Не удалось получить значение обязательного параметра {constructorParam.Name}");
							}
						}
					}
				}

				var cmd = IoC.Resolve<ICommand>(action, prms);

				var queue = IoC.Resolve<ConcurrentQueue<ICommand>>("QueueCommand.Get");

				queue.Enqueue(cmd);
			}
		}
	}
}

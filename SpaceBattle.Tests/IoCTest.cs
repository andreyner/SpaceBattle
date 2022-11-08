using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpaceBattle.Repository;
using SpaceBattle.Repository.Container;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpaceBattle.Tests
{
	[TestClass]
	public class IoCTest
	{
		/// <summary>
		/// Регистрация движущегося объекта, адаптера и комманды движения. Затем необходимо разрешешить эти объектов
		/// </summary>
		[TestMethod]
		public void RegisterResolveMoveCommand()
		{
			//Установка базовых комманд и скоупа
			new RegisterBaseCommand(scopeId: "default1").Execute();
			IContainer ioc = new IoC();

			ioc.Resolve<ICommand>("IoC.Register", "UObject", (Func<object[], object>)((args) =>
			{
				var uObject = new Mock<Uobject>();
				return uObject.Object;
			})).Execute();

			ioc.Resolve<ICommand>("IoC.Register", "MovableAdapter", (Func<object[], object>)((args) =>
			{
				var uObject = (Uobject)args[0];
				return new MovableAdapter(uObject);
			})).Execute();

			ioc.Resolve<ICommand>("IoC.Register", "Move", (Func<object[], object>)((args) =>
			{
				return new Move((IMovable)args[0]);
			})).Execute();

			var uObject =  ioc.Resolve<Uobject>("UObject");
			var movableadapter = ioc.Resolve<MovableAdapter>("MovableAdapter", uObject);
			var move = ioc.Resolve<ICommand>("Move", movableadapter);

			Assert.IsNotNull(move);
		}

		/// <summary>
		/// Создать новый скоуп с иименем "22" и проверить его наличие
		/// </summary>
		[TestMethod]
		public void CreateNewScope()
		{
			//Установка базовых комманд и скоупа
			new RegisterBaseCommand(scopeId: "default2").Execute();
			IContainer ioc = new IoC();

			ioc.Resolve<ICommand>("Scopes.New", "22").Execute();

			Assert.IsTrue(ScopeRepository.Value.repository.ContainsKey("22"));
		}

		/// <summary>
		/// Создать новый скоуп с иименем "2" и установить как текущий
		/// </summary>
		[TestMethod]
		public void SetCurrentScope()
		{
			//Установка базовых комманд и скоупа
			new RegisterBaseCommand(scopeId: "default3").Execute();
			IContainer ioc = new IoC();

			ioc.Resolve<ICommand>("Scopes.New", "2").Execute();
			ioc.Resolve<ICommand>("Scopes.Current", "2").Execute();

			Assert.IsTrue(ScopeRepository.Value.CurrentScope.Value.Id == "2");
		}


		/// <summary>
		/// Создать 3 потока, в каждом потоке создать скоуп, зарегистрировать и разрешить зависимости
		/// </summary>
		[TestMethod]
		public void MultiThreadTest()
		{
			//Создание дефолтного скоупа и добавление базовых комманд
			new RegisterBaseCommand(scopeId: "1").Execute();
			IContainer ioc = new IoC();

			Func<string, Uobject> func = (scoped) =>
			{
				//Создание скоупа и добавление базовых комманд
				new RegisterBaseCommand(scoped).Execute();

				ioc.Resolve<ICommand>("IoC.Register", "UObject", (Func<object[], object>)((args) =>
				{
					var uObject = new Mock<Uobject>();
					return uObject.Object;
				})).Execute();

				return ioc.Resolve<Uobject>("UObject");
			};

			var firstScopeName = "firstScope";
			var secondScopeName = "secondScope";
			var thirdScopeName = "thirdScope";

			Task<Uobject> t1 = Task.Run(() =>
			{
				return func(firstScopeName);
			});
			Task<Uobject> t2 = Task.Run(() =>
			{
				return func(secondScopeName);
			});
			Task<Uobject> t3 = Task.Run(() =>
			{
				return func(thirdScopeName);
			});

			Task.WaitAll(t1, t2, t3);

			Assert.IsTrue(ScopeRepository.Value.repository.ContainsKey(firstScopeName) &&
				ScopeRepository.Value.repository.ContainsKey(secondScopeName) &&
				ScopeRepository.Value.repository.ContainsKey(thirdScopeName) &&
				t1.Result != null &&
				t2.Result != null &&
				t3.Result != null );
		}

	}
}

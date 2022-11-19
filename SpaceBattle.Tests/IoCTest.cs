using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SpaceBattle.Repository;
using SpaceBattle.Repository.Commands;
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
		[TestInitialize]
		public void TestInitialize()
		{
			new InitScopesCommand().Execute();
		}

		[TestMethod]
		public void RootScopeIsAvalible()
		{
			Assert.IsNotNull(IoC.Resolve<object>("Scopes.Root"));
		}

		[TestMethod]
		public void CreateNewScopeIsPossibleAtAnyMoment()
		{
			Assert.IsNotNull(IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root")));
		}

		[TestMethod]
		public void RegisterResolveDependency()
		{

			IoC.Resolve<ICommand>(
				"Scopes.Current.Set",
				IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"))
				).Execute();

			IoC.Resolve<ICommand>(
				"IoC.Register",
				"dependency",
				(Func<object[], object>)((args) => 1)).Execute();

			Assert.AreEqual(1, IoC.Resolve<int>("dependency"));
		}


		[TestMethod]
		public void ResolveDependencyOnCurrentScope()
		{
			var scope = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));

			IoC.Resolve<ICommand>("Scopes.Current.Set", scope).Execute();

			IoC.Resolve<ICommand>(
				"IoC.Register",
				"dependency",
				(Func<object[], object>)((args) => 1)).Execute();

			Assert.AreEqual(1, IoC.Resolve<int>("dependency"));

			var scope2 = IoC.Resolve<object>("Scopes.New", IoC.Resolve<object>("Scopes.Root"));

			IoC.Resolve<ICommand>("Scopes.Current.Set", scope2).Execute();

			IoC.Resolve<ICommand>(
				"IoC.Register",
				"dependency",
				(Func<object[], object>)((args) => 2)).Execute();

			Assert.AreEqual(2, IoC.Resolve<int>("dependency"));

			IoC.Resolve<ICommand>("Scopes.Current.Set", scope).Execute();

			Assert.AreEqual(1, IoC.Resolve<int>("dependency"));
		}
	}
}

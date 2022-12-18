using SpaceBattle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceBattle.TokenApp
{
	public class Users
	{
        public List<Person> people = new List<Person>
        {
            new Person {Login="admin@gmail.com", Password="12345", Role = "admin" },
            new Person { Login="qwerty@gmail.com", Password="55555", Role = "user" },
            new Person { Login="qwerty1@gmail.com", Password="55555", Role = "user" },
            new Person { Login="qwerty2@gmail.com", Password="55555", Role = "user" },
            new Person { Login="qwerty3@gmail.com", Password="55555", Role = "user" },
            new Person { Login="qwerty4@gmail.com", Password="55555", Role = "user" }
        };
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpaceBattle.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace SpaceBattle.TokenApp
{
	[Controller]
	public class GameController : Controller
	{
		private readonly ButtleStorage gameStorage;
		private readonly Users users;

		public GameController(ButtleStorage gameStorage, Users users)
		{
			this.gameStorage = gameStorage;
			this.users = users;
		}

		/// <summary>
		/// Создать игру
		/// </summary>
		/// <param name="playerEmails">почты игроков</param>
		/// <returns>Ид боя</returns>
		[Authorize]
		[HttpPost]
		public Guid Create([FromBody] Players players)
		{
			gameStorage.Buttles ??= new List<Buttle>();

			var id = Guid.NewGuid();

			gameStorage.Buttles.Add(new Buttle
			{
				Id = id,
				Players = players.Emails.ToList()
			});

			return id;
		}

		/// <summary>
		/// Получить токен доступа к игре
		/// </summary>
		/// <param name="buttleId"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPost]
		public IActionResult GetTokenGame(Guid buttleId)
		{
			var userEmail = HttpContext.User.Identity.Name;

			var game = gameStorage.Buttles.FirstOrDefault(x => x.Id == buttleId && x.Players.Contains(userEmail));

			if(game == null)
			{
				throw new Exception("Игра не найдена!");
			}

			return Token(userEmail, buttleId);
		}

		/// <summary>
		/// Присоединиться к игре используя токен
		/// </summary>
		/// <param name="buttleId"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPost]
		public IActionResult TryJoinGame(Guid buttleId)
		{
			var userEmail = HttpContext.User.Identity.Name;

			var gameClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "GameId" && x.Value == buttleId.ToString());

			if (gameClaim == null) 
			{
				return BadRequest($"Не найдено разрешения на игру {buttleId}!");
			}

		   var game = gameStorage.Buttles.FirstOrDefault(x => x.Id == buttleId && x.Players.Contains(userEmail));

			return Ok("Подключение к игре успешно выполнено");
		}

		private IActionResult Token(string username, Guid gameId)
		{
			var identity = GetIdentity(username, gameId);
			if (identity == null)
			{
				return BadRequest(new { errorText = "Invalid username" });
			}

			var now = DateTime.UtcNow;
			// создаем JWT-токен
			var jwt = new JwtSecurityToken(
					issuer: AuthOptions.ISSUER,
					audience: AuthOptions.AUDIENCE,
					notBefore: now,
					claims: identity.Claims,
					expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
					signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			var response = new
			{
				access_token = encodedJwt,
				username = identity.Name
			};

			return Json(response);
		}

		private ClaimsIdentity GetIdentity(string username, Guid gameId)
		{
			Person person = users.people.FirstOrDefault(x => x.Login == username);
			if (person != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
					new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role),
					new Claim("GameId", gameId.ToString())
				};
				ClaimsIdentity claimsIdentity =
				new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
					ClaimsIdentity.DefaultRoleClaimType);
				return claimsIdentity;
			}

			// если пользователя не найдено
			return null;
		}
	}
}

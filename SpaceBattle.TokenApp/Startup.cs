using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SpaceBattle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceBattle.TokenApp
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddSingleton<ButtleStorage>();
			services.AddSingleton<Users>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					// укзывает, будет ли валидироваться издатель при валидации токена
					ValidateIssuer = true,
					// строка, представляющая издателя
					ValidIssuer = AuthOptions.ISSUER,

					// будет ли валидироваться потребитель токена
					ValidateAudience = true,
					// установка потребителя токена
					ValidAudience = AuthOptions.AUDIENCE,
					// будет ли валидироваться время существования
					ValidateLifetime = true,

					// установка ключа безопасности
					IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
					// валидация ключа безопасности
					ValidateIssuerSigningKey = true
				};
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseAuthentication();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				// определение маршрутов
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action}");
			});
		}
	}
}

using AmberArchives.Controllers;
using AmberArchives.Entities;
using AmberArchives.Middleware;
using AmberArchives.Models;
using AmberArchives.Models.Validators;
using AmberArchives.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberArchives
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			var authenticatonSettings = new AuthenticationSettings();
			Configuration.GetSection("Authentication").Bind(authenticatonSettings);

			services.AddSingleton(authenticatonSettings);
			services.AddAuthentication(option =>
			{
				option.DefaultAuthenticateScheme = "Bearer";
				option.DefaultScheme = "Bearer";
				option.DefaultChallengeScheme = "Bearer";
			}).AddJwtBearer(cfg =>
			{
				cfg.RequireHttpsMetadata = false;
				cfg.SaveToken = true;
				cfg.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = authenticatonSettings.JwtIssuer,
					ValidAudience = authenticatonSettings.JwtIssuer,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticatonSettings.JwtKey)),
				};

			});
			services.AddControllers().AddFluentValidation();
			services.AddDbContext<AmberArchivesDbContext>();
			services.AddScoped<AmberArchivesSeeder>();
			services.AddAutoMapper(this.GetType().Assembly);
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<IEditionService, EditionService>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<ErrorHandlingMiddleware>();
			services.AddScoped<RequestTimeMiddleware>();
			services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
			services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
			services.AddScoped<IValidator<BookQuery>, BookQueryValidator>();
			services.AddScoped<IUserContextService, UserContextService>();
			services.AddHttpContextAccessor();
			services.AddSwaggerGen();
			services.AddCors(options =>
			{
				options.AddPolicy("FrontEndClient", builder =>

				builder.AllowAnyMethod()
				.AllowAnyHeader()
				.WithOrigins(Configuration["AllowedOrigins"])
			);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AmberArchivesSeeder seeder)
		{
			app.UseStaticFiles();
			app.UseCors("FrontEndClient"); 
			seeder.Seed();
			
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseMiddleware<ErrorHandlingMiddleware>();
			app.UseMiddleware<RequestTimeMiddleware>();
			app.UseAuthentication();
			app.UseHttpsRedirection();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "AmberArchives API");
			});

			app.UseRouting();
			app.UseAuthorization();						
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();

			});
		}
	}
}

using AmberArchives.Controllers;
using AmberArchives.Entities;
using AmberArchives.Middleware;
using AmberArchives.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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
			services.AddControllers();
			services.AddDbContext<AmberArchivesDbContext>();
			services.AddAutoMapper(this.GetType().Assembly);
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<IEditionService, EditionService>();
			services.AddScoped<ErrorHandlingMiddleware>();
			services.AddSwaggerGen();
			services.AddScoped<RequestTimeMiddleware>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			app.UseMiddleware<ErrorHandlingMiddleware>();
			app.UseMiddleware<RequestTimeMiddleware>();
			app.UseHttpsRedirection();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "AmberArchives API");
			});

			app.UseRouting();
						
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();

			});
		}
	}
}

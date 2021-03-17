using Core5_API.Models;
using Core5_API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core5_API
{
	public class Startup
	{
		/// <summary>
		/// The IConfigiration is used to read global config level settings from
		/// appsettings.json file
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		/// <summary>
		/// CLass to register all Application level Services
		/// The method privides DI COntainer with Lifetime for Registered Objects
		/// Singleton, Scopped and Transient
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices(IServiceCollection services)
		{
			// register the  Data Access Layer aka EF Core
			services.AddDbContext<CompanyContext>(options=> {
				options.UseSqlServer(Configuration.GetConnectionString("DbConnStr"));
			});


			// register custom services in DI
			services.AddScoped<IService<Department, int>, DepartmentService>();


			// the request for teh API ontroller to create and instance of ApiController to map 
			// action methods with HTTP Request Type aka Http Verb
			services.AddControllers();

			// expose the Swagger json for metadata expore towards client appss
			// Connected Service Contract Sharing for Web/Desktop/Browser/Mobile clients
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core5_API", Version = "v1" });
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core5_API v1"));
			}

			app.UseHttpsRedirection();
			// Routing Middleware to Evaluate Incomming Request URL and Map with COntroller and actions
			app.UseRouting();
			// USed for Role-Based-Security
			app.UseAuthorization();

			// Subscribe to HTTP EndPoint and Map the Incomming URL with COntroller and Execute it
			// using all middlewraes loaded in Pipekline
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}

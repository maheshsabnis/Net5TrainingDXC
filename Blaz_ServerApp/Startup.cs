using Blaz_ServerApp.Areas.Identity;
using Blaz_ServerApp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blaz_ServerApp.Models;
using Blaz_ServerApp.Services;

namespace Blaz_ServerApp
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("DefaultConnection")));

			services.AddDbContext<CompanyContext>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("AppConnStr")));



			// ASP.NET Core 5 Identity APIs
			services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			// Custom Services Registration
			services.AddScoped<IService<Department, int>, DepartmentService>();


			// Manage request processing for Razor Pages (An Alternative to Web Forms from .NET 5)
			services.AddRazorPages();
			// Manage requets processing for Blazor Web Conponents(?) on Server-Side with Each PostBack
			// using SignalR
			services.AddServerSideBlazor();
			// the Service for Managing he User BAsed Authentication
			services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
			// Database Error Page for Identity to run the migrations
			services.AddDatabaseDeveloperPageExceptionFilter();

			services.AddSingleton<WeatherForecastService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				// Execute the Blazor PoastBack for Handling an Event, 
				// Executing Components, and serializing the new generated DOM
				// on client using the SignalR Socker Hub
				endpoints.MapBlazorHub();
				
				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}

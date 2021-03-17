using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// enabling the OpenAPI support for teh REST API in ASP.NET Core 5
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Core5_API
{
	public class Program
	{
		/// <summary>
		/// Entry POint for App
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			// Integrated with Hosting Env. (IIS / Apache / Docker / NGinx)
			CreateHostBuilder(args).Build().Run();
		}
		/// <summary>
		/// IHostBuilder
		/// Contract between the Application Object Model with the Hosting Env  for
		/// 1. Security
		/// 2. Database Connections
		/// 3. Sessions
		/// 4. CORS
		/// 5. Cookies
		/// 6. Custom Services (Singleton / Scoppesd / Transient) actiovations of objects
		/// 7. HTTP Pipeline for specific Resource
		///		- MVC and API Controller
		///			- Filtres
		///		- API
		///			- Filters
		///		- RazorPages
		///		- Blazor
		///Resolves IApplicationBuilder and IHostingEnvironment		
		/// IHostingEnvironment, detect the execution env. of App and accodingly load the objects in pipeline
		/// IApplicationBuilder, Handle the HttpRequest under HttpContext
		/// HttpContext, represents the current request
		///		- All middlewares(?) will be used during processing the request
		///		- LOads all Middlewares and handle the request p[rocessing
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					// The StartUp class for all App level Initialization (HttpApplication)
					// UseStartup<Startup>() method  is typed to the Startup class
					// this resolved the reference for IConfiguration interface
					webBuilder.UseStartup<Startup>();
				});
	}
}

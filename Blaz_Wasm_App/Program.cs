using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Blazored.LocalStorage;
 
namespace Blaz_Wasm_App
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			// create an Application Model of all resources
			// 1. Components, 
			// 2. CSS
			// 3. Other Staic Files
			// 4. Depednencies
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			// App is the default Application Object
			// COllection all DOM Resources and mount on the 'index.html'
			// having a <div> with 'id' as 'app'
			// the RootComponent is 'app' and under that all other Resourses 
			// are loaded (Rdenring / Events /Databinding)
			builder.RootComponents.Add<App>("#app");

			// Depednency Management
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
			// adding the session storage
			builder.Services.AddBlazoredSessionStorage();
			// adding the LocalStorage
			builder.Services.AddBlazoredLocalStorage();
			// register the state container as global object
			builder.Services.AddSingleton<AppStateContainer>();

			await builder.Build().RunAsync();
		}
	}
}

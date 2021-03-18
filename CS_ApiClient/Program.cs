using OpenAPIClient;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace CS_ApiClient
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Console.WriteLine("Press Any Key when Service STarts");
			Console.ReadLine ();

			// provide HTTP Request Metainfo e.g. Base Url, Header info, etc.
			var httpClient = new HttpClient();

			var client = new  DepartmentClient("https://localhost:44328/", httpClient);
			var res = await client.GetallAsync();

			Console.WriteLine(JsonSerializer.Serialize(res));

			Console.WriteLine();

			Department d = new Department()
			{
			  DeptNo=9002, DeptName="Dept_9002", Location="Pune"		
			};

			var data = await client.CreateoneAsync(d);


			Console.WriteLine(JsonSerializer.Serialize(data));

			Console.WriteLine("Hello World!");
		}
	}
}

using System;
using System.Net.Http;

namespace CS_ApiClient
{
	class Program
	{
		static void Main(string[] args)
		{

			var httpClient = new HttpClient();
			var apiClient = new OpentApiClient.DepartmentAPIClient("",httpClient);
			var res = apiClient.DepartmentAPIAsync();
			Console.WriteLine("Hello World!");
		}
	}
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core5_API.Middleware
{
	public class ErrorClass
	{
		public int ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
	}

	public class ErrorHandler
	{
		private readonly RequestDelegate _next; 
		public ErrorHandler(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = 500;
				var errorInfo = new ErrorClass()
				{
					 ErrorCode = context.Response.StatusCode,
					 ErrorMessage = ex.Message
				};

				string response = System.Text.Json.JsonSerializer.Serialize(errorInfo);
				await context.Response.WriteAsJsonAsync(errorInfo);
			}
		}
	}

	public static class CustomMiddleware
	{
		public static void UseErrorMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ErrorHandler>();
		}
	}
}

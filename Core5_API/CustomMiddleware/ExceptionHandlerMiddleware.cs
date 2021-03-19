using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core5_API.CustomMiddleware
{
	/// <summary>
	/// Value Object or DTO that will contain Error Infromation
	/// </summary>
	public class ErrorResponse
	{
		public int ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
	}

	/// <summary>
	/// Class for contains logic for Error Handling in the Middleware
	/// 1. The class must be ctor injected using RequestDelegate
	/// </summary>
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		/// <summary>
		/// Help to instantiated the class inside the HttpContext in current HttpRequest
		/// </summary>
		/// <param name="next"></param>
		public ExceptionHandlerMiddleware(RequestDelegate next)
		{
			_next = next;
		}
		/// <summary>
		/// the standard method name in Custom Middleware class
		/// Generally it is Async method
		/// THis method will accept HttpContext object 
		/// This method will contain logic
		/// </summary>
		/// <returns></returns>
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				// if no exeception then move to next Middleware in pipeline
				await _next(context);
			}
			catch (Exception ex)
			{
				// otherwsie handle expception

				// a. define an error code for the response
				context.Response.StatusCode = 500;
				// configure the error response
				var errorResponse = new ErrorResponse()
				{
					 ErrorCode = context.Response.StatusCode,
					 ErrorMessage = ex.Message
				};

				// serialize the response for the request
				// (alternatively log it on server as per your choice)
				await context.Response.WriteAsJsonAsync(errorResponse);
			}
		}
	}

	/// <summary>
	/// The class that will be used to define an extension method
	/// to register a custom middleware
	/// </summary>
	public static class CustomErrorMiddleware
	{
		public static void UseErrorMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionHandlerMiddleware>();
		}
	}
	 
}

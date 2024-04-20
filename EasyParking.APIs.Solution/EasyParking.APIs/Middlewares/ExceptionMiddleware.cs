﻿using EasyParking.APIs.Errors;
using System.Net;
using System.Text.Json;

namespace EasyParking.APIs.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate next;
		private readonly ILogger<ExceptionMiddleware> logger;
		private readonly IHostEnvironment env;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
			this.next = next;
			this.logger = logger;
			this.env = env;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await next.Invoke(context);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);

				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

				//var options = new JsonSerializerOptions()
				//{
				//	PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				//};
				var response = env.IsDevelopment() ?
							   new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
								  :
							   new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message);

				var Options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
				var json = JsonSerializer.Serialize(response, Options);

				await context.Response.WriteAsync(json);
			}
		}
    }
}

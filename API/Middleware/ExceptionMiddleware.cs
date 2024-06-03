using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync (HttpContext context)
        {
            try 
            {
                await _next (context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex , ex.Message); //logging output or console in exception | ex : exception | ex.message : exception message 
                context.Response.ContentType = "application/json"; //all the response sent in json format 
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError; // set the Statuscode to InternalServerError which is statuscode of 500 

                var reponse = _env.IsDevelopment() // check if its in developement 
                    ? new ApiException ((int) HttpStatusCode.InternalServerError, ex.Message, //for developement | stacktrace for details | ToString to output will be string 
                    ex.StackTrace.ToString())
                    : new ApiException ((int) HttpStatusCode.InternalServerError); // in production 

                var options = new JsonSerializerOptions {PropertyNamingPolicy = JsonNamingPolicy.CamelCase}; //convert into CamelCase 

                var json = JsonSerializer.Serialize(reponse, options); // serialize into json format

                await context.Response.WriteAsync(json);


            }
        }
    }
}
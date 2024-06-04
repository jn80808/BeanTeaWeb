using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using SQLitePCL;

namespace API.Errors
{
    public class ApiResponse
    {

        public ApiResponse(int statusCode, string message = null) 
        {
            this.StatusCode = statusCode;
            this.Message = message ?? GetDefaultMessageForStatusCode(statusCode);
   
        }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch 
            {
                400 => "A bad request , Input Id Is Invalid",
                401 => "Un-Autorized",
                404 => "Request Nor found",
                500 => "Not Found, Not exist ",
                _  => null
            };
        }

        public int StatusCode { get; set; }

        public string Message { get; set; }
    }
}
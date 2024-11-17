using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class ClaimsPrinicipleExtensions
    {
        public static string RetrieveEmailFromPrinciple (this ClaimsPrincipal user)
        {
            return user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        }
    }
}
using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace LibraryAPI
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RequestLimitAttribute : ActionFilterAttribute
    {
        public RequestLimitAttribute(string name)
        {
            Name = name;
        }
        public string Name { get; }
        public int NoOfRequest { get; set; } = 2;
        public int Seconds { get; set; } = 10;
        private static MemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions());
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ipAddress = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress;
            var memoryCacheKey = $"{Name}-{ipAddress}";
            Cache.TryGetValue(memoryCacheKey, out int prevReqCount);
            if (prevReqCount >= NoOfRequest)
            {
                context.Result = new ContentResult
                {
                    Content = $"Request limit is exceeded. Try again in {Seconds} seconds.",
                };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
            }
            else
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(Seconds));
                Cache.Set(memoryCacheKey, (prevReqCount + 1), cacheEntryOptions);
            }
        }
    }
}
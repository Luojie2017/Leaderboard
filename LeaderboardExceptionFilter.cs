using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace CustomerDemo
{
    /// <summary>
    /// 异常过滤器，统一捕获Controller中的异常
    /// </summary>
    public class LeaderboardExceptionFilter : IAsyncExceptionFilter
    {
        private readonly IWebHostEnvironment env;
        public LeaderboardExceptionFilter(IWebHostEnvironment env)
        {
            this.env = env;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var errorMsg = this.env.IsDevelopment() ? context.Exception.ToString() : "An unhandled exception occurred on the server side.";
            context.Result = new BadRequestObjectResult(errorMsg);
            context.ExceptionHandled = true;
            return Task.CompletedTask;
        }
    }
}

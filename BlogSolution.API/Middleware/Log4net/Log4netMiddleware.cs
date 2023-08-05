using log4net.Config;
using log4net;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using BlogSolution.Utilities.Common;

namespace BlogSolution.API.Middleware.Log4net
{
    public class Log4netMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILog _logger;

        public Log4netMiddleware(RequestDelegate next, ILog logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // log server
                Console.WriteLine("Error is: {0}", ex.Message);
                // write file
                _logger.Error(ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                //response error
                var result = new BaseResultModel
                {
                    IsSuccess = false,
                    Code = (int)HttpStatusCode.InternalServerError,
                    Message = ex.Message
                };
                await context.Response.WriteAsJsonAsync(result);
            }
        }

    }
}

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using R2ETuan.NETCore.QuachVanViet.Services.Logging;

namespace R2ETuan.NETCore.QuachVanViet.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly IRequestLogger _requestLogger; 

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger, IRequestLogger requestLogger)
        {
            _next = next;
            _logger = logger;
            _requestLogger = requestLogger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Enable buffering for multiple reads of request body
            context.Request.EnableBuffering();

            //Read request details
            var scheme = context.Request.Scheme; //"http" or "https"
            var host = context.Request.Host.ToString(); 
            var path = context.Request?.Path.ToString();
            var queryString = context.Request?.QueryString.ToString();

            //Read request body
            string requestBody = string.Empty;
            if (context.Request!.Body.CanRead)
            {
                using (var reader = new StreamReader(
                   context.Request.Body,
                   encoding: Encoding.UTF8,
                   detectEncodingFromByteOrderMarks: false,
                   leaveOpen: true))
                {
                    requestBody = await reader.ReadToEndAsync();
                    //Reset stream position for downstream middleware/controllers
                    context.Request.Body.Position = 0;
                }

                await _requestLogger.LogRequestAsync(scheme, host, path, queryString, requestBody);

                await _next(context);
            }
        }
    }
}

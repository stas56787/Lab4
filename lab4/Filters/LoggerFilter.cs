using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4.Filters
{
    public class LoggerFilter : Attribute, IResourceFilter
    {
        ILogger _logger;
        public LoggerFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("LoggerFilter");
        }
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _logger.LogInformation($" закончил работу {context.ActionDescriptor.DisplayName} - {DateTime.Now}");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _logger.LogInformation($" начал работу {context.ActionDescriptor.DisplayName} - {DateTime.Now}");
        }
    }
}

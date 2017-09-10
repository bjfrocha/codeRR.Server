﻿using System.Collections.Generic;
using System.Web.Http.ExceptionHandling;
using log4net;
using OneTrueError.Client;
using OneTrueError.Client.Contracts;

namespace OneTrueError.Web.Infrastructure.Logging
{
    internal class WebApiLogger : ExceptionLogger
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(WebApiLogger));

        public string ReportToOneTrueError { get; set; }

        public override void Log(ExceptionLoggerContext context)
        {
            var data = "";
            if (context.Request.Content != null)
            {
                data = context.Request.Content.ReadAsStringAsync().Result;
            }

            _logger.Error("Request + " + context.Request.RequestUri + ", data" + data, context.Exception);
            _logger.Error(context.Exception);

            if (!WebApiApplication.ReportToOneTrueError)
                return;

            var properties = new Dictionary<string, string>
            {
                {"Url", context.Request.RequestUri.ToString()},
                {"HttpMethod", context.Request.Method.Method}
            };
            if (context.Request.Headers.Referrer != null)
                properties.Add("Referer", context.Request.Headers.Referrer.ToString());
            if (data.Length < 30000)
                properties.Add("Body", data);
            var collection = new ContextCollectionDTO("Request", properties);
            OneTrue.Report(context.Exception, collection);
            base.Log(context);
        }
    }
}
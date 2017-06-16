﻿using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Tinifier.Core.Infrastructure.Exceptions;
using Umbraco.Core.Logging;

namespace Tinifier.Core.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            Exception ex = context.Exception;

            if (context.Exception is EntityNotFoundException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }

            if (context.Exception is HttpRequestException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            if (context.Exception is WebException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadGateway, ex.Message);
            }

            LogHelper.Error(GetType(), ex.StackTrace, ex);
            base.OnException(context);
        }
    }
}

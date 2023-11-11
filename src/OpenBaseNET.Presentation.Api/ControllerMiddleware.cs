﻿using System.Text;
using OpenBaseNET.Domain.Context;

namespace OpenBaseNET.Presentation.Api;

public class ControllerMiddleware
{
    private readonly RequestDelegate _next;

    public ControllerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, SessionContext sessionContext)
    {
        sessionContext.Correlationid = Guid.NewGuid();
        sessionContext.Host = context.Request.Host.Value;
        sessionContext.Path = context.Request.Path.Value ?? "";
        sessionContext.QueryString = context.Request.QueryString.Value ?? "";
        sessionContext.Method = context.Request.Method;
        if (context.Request.ContentLength > 0)
        {
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body,
                Encoding.UTF8, false,
                leaveOpen: true);
            sessionContext.Body = await reader.ReadToEndAsync();
            context.Request.Body.Seek(0, SeekOrigin.Begin);
        }

        sessionContext.Headers
            = context.Request.Headers.ToDictionary(x =>
                x.Key, x => x.Value.ToString());
        await _next(context);
    }
}
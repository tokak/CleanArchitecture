﻿
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using FluentValidation;

namespace CleanArchitecture.WepApi.Middleware;

public sealed class ExceptionMiddleware : IMiddleware
{
    private readonly AppDbContext _context;

    public ExceptionMiddleware(AppDbContext context)
    {
        _context = context;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await LogExceptionToDatabaseAsync(ex, context.Request);
            await HandleExcaptionAsync(context, ex);
        }
    }

    private Task HandleExcaptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;
        if (ex.GetType() == typeof(ValidationException))
        {
            return context.Response.WriteAsync(new ValidationErrorDetails
            {
                Errors = ((ValidationException)ex).Errors.Select(s => s.PropertyName),
                StatusCode = 403
            }.ToString());
        }
        return context.Response.WriteAsync(new ErrorResult
        {
            Message = ex.Message,
            StatusCode = context.Response.StatusCode
        }.ToString());
    }

    private async Task LogExceptionToDatabaseAsync(Exception ex,HttpRequest httpRequest)
    {
       ErrorLog errorLog = new ErrorLog()
       {
           ErrorMessage = ex.Message,
           StackTrace = ex.StackTrace,
           RequestPath = httpRequest.Path,
           RequestMethod = httpRequest.Method,
           Timestamp = DateTime.Now
       };
        await _context.Set<ErrorLog>().AddAsync(errorLog,default);
        await _context.SaveChangesAsync(default);
    }
}

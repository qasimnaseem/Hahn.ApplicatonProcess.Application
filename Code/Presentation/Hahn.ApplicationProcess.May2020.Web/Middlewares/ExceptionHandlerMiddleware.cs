using Hahn.ApplicationProcess.May2020.Application.Common.Exceptions;
using Hahn.ApplicationProcess.May2020.Domain.Common;
using Hahn.ApplicationProcess.May2020.Domain.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.May2020.Web.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var errorDto = new ErrorDTO(exception.Message);

            switch (exception)
            {
                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    errorDto.MoreInfo = validationException.Failures;
                    break;
                case BadRequestException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case NotFoundException _:
                    code = HttpStatusCode.NotFound;
                    break;
                case AlreadyExistException _:
                    code = HttpStatusCode.UnprocessableEntity;
                    break;
                default:
                    _logger.Error(exception, exception.Message); //log error only when its unhandled exception
                    errorDto.Error = AppConstants.ErrorMessages.GenericError;
                    break;
            }

            var jsonSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var jsonResponse = JsonConvert.SerializeObject(errorDto, jsonSettings);
            context.Response.ContentType = AppConstants.ContentTypes.Json;
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(jsonResponse);
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}

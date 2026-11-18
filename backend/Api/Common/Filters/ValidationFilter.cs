namespace Api.Common.Filters;

using FluentValidation;

public sealed class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator is null) return await next(context);

        var argument = context.Arguments.OfType<T>().FirstOrDefault();
        if (argument is null)
            return Results.Problem(statusCode: StatusCodes.Status400BadRequest,
                                   title: "Validation", detail: "The request is missing or malformed.");

        var result = await validator.ValidateAsync(argument, context.HttpContext.RequestAborted);
        return result.IsValid
            ? await next(context)
            : Results.ValidationProblem(result.ToDictionary());   // 400 with an "errors" object
    }
}
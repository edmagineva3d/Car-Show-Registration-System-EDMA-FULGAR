namespace Api.Features.Registrations;

using Api.Common.Extensions;
using Api.Common.Filters;
using Application.Abstractions.Messaging;
using Application.Features.Registrations;
using Application.Features.Registrations.CreateRegistration;
using Application.Features.Registrations.GetRegistrationById;
using Application.Features.Registrations.GetRegistrations;
using Application.Features.Registrations.GetRegistrationSummary;
using Domain.Common;

public static class RegistrationEndpoints
{
    public static void MapRegistrationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/registrations").WithTags("Registrations");

        group.MapPost("/", Create)
            .WithName("CreateRegistration").WithSummary("Make a new registration")
            .AddEndpointFilter<ValidationFilter<CreateRegistrationCommand>>();

        group.MapGet("/", GetAll)
            .WithName("GetRegistrations").WithSummary("Show all registrations (paged, filterable)")
            .AddEndpointFilter<ValidationFilter<GetRegistrationsQuery>>();

        group.MapGet("/summary", GetSummary)
            .WithName("GetRegistrationSummary").WithSummary("KPI totals for the dashboard");

        // {id:guid}: "summary" and "abc" never match this route (cases 31, 38)
        group.MapGet("/{id:guid}", GetById)
            .WithName("GetRegistrationById").WithSummary("Find registration by ID");
    }

    private static async Task<IResult> Create(
        CreateRegistrationCommand command,
        ICommandHandler<CreateRegistrationCommand, Result<RegistrationResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(command, cancellationToken);
        return result.IsSuccess
            ? TypedResults.Created($"/api/registrations/{result.Value.Id}", result.Value)
            : result.ToProblemDetails();
    }

    private static async Task<IResult> GetAll(
        [AsParameters] GetRegistrationsQuery query,
        IQueryHandler<GetRegistrationsQuery, Result<GetRegistrationsResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(query, cancellationToken);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ToProblemDetails();
    }

    private static async Task<IResult> GetSummary(
        IQueryHandler<GetRegistrationSummaryQuery, Result<GetRegistrationSummaryResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new GetRegistrationSummaryQuery(), cancellationToken);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ToProblemDetails();
    }

    private static async Task<IResult> GetById(
        Guid id,
        IQueryHandler<GetRegistrationByIdQuery, Result<RegistrationResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(new GetRegistrationByIdQuery(id), cancellationToken);
        return result.IsSuccess ? TypedResults.Ok(result.Value) : result.ToProblemDetails();
    }
}
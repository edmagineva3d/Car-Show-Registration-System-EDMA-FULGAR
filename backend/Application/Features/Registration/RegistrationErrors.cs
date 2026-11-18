namespace Application.Features.Registrations;

using Domain.Common;

public static class RegistrationErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("Registration.NotFound", $"Registration '{id}' was not found.");

    public static Error DuplicatePlate(string plate) =>
        new("Registration.Conflict", $"A registration with plate number '{plate}' already exists.");
}
namespace Domain.Common;

using Domain.Enums;

public static class RegistrationFees
{
    public static decimal For(RegistrationType type) => type switch
    {
        RegistrationType.Exhibitor => 1_500m,
        RegistrationType.Competitor => 2_500m,
        RegistrationType.VIP => 5_000m,
        _ => throw new ArgumentOutOfRangeException(nameof(type))
    };
}
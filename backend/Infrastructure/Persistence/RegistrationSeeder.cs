namespace Infrastructure.Persistence;

using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

public static class RegistrationSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Registrations.AnyAsync()) return;   // never seed twice (case 39)

        string[] names =
        {
            "Juan Dela Cruz","Ana Reyes","Mark Santos","Liza Garcia","Paolo Mendoza",
            "Grace Villanueva","Carlo Ramos","Joy Navarro","Ken Bautista","Mia Fernandez",
            "Rico Aquino","Tina Castillo","Nico Torres","Bea Domingo","Leo Pascual",
            "Faith Salazar","Jun Flores","Rose Dizon","Gabriel Cruz","Hannah Lim",
            "Migo Soriano","Cathy Ocampo","Dan Mercado","Ella Tan","Ivan Padilla"
        };
        string[] cars = { "Toyota Supra MK4", "Nissan Skyline R34", "Honda Civic EK9", "Mazda RX-7 FD", "Mitsubishi Lancer Evo IX" };
        var types = Enum.GetValues<RegistrationType>();
        var statuses = new[] { RegistrationStatus.Confirmed, RegistrationStatus.CheckedIn,
                               RegistrationStatus.Pending, RegistrationStatus.Cancelled };
        var start = DateTime.UtcNow.AddDays(-30);

        for (var i = 1; i <= 25; i++)
        {
            var type = types[(i - 1) % 3];
            var fee = RegistrationFees.For(type);
            var paid = (((i - 1) / 3) % 3) switch { 0 => 0m, 1 => Math.Round(fee / 2, 2), _ => fee };

            db.Registrations.Add(new Registration
            {
                Id = Guid.NewGuid(),
                RegistrationNumber = $"CS27-{i:D4}",
                RegistrationDate = start.AddHours(i * 6),
                ParticipantName = names[i - 1],
                ContactNumber = $"0917{1_000_000 + i}",
                Email = $"participant{i}@example.com",
                Address = $"{i} Rizal St., Sorsogon City",
                CarMakeModel = cars[(i - 1) % cars.Length],
                PlateNumber = $"SEED {i:D4}",
                RegistrationType = type,
                Status = statuses[(i - 1) % 4],
                RegistrationFee = fee,
                AmountPaid = paid
            });
        }
        await db.SaveChangesAsync();
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CleanProject.Domain.Entities;

public sealed class CreditCard : BaseEntity
{
    [StringLength(16)] [Required] public required string Number { get; init; }
    [StringLength(3)] [Required] public required string Cvv { get; init; }

    public DateTime ExpirationDate { get; init; }
    [JsonIgnore] public Account Account { get; init; }

    [ForeignKey("Account")] public Guid AccountId { get; init; }


    public static CreditCard Generate(Guid accountId)
    {
        var random = new Random();
        var cardNumber = "4"; // Visa cards start with '4'

        // Generate the next 15 random digits
        for (var i = 0; i < 15; i++) cardNumber += random.Next(0, 10).ToString();

        var newCreditCard = new CreditCard
        {
            Number = cardNumber,
            Cvv = random.Next(100, 900).ToString(),
            ExpirationDate = DateTime.UtcNow.AddYears(3),
            AccountId = accountId // Set the foreign key properly
        };
        return newCreditCard;
    }
}
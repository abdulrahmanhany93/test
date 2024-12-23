using System.ComponentModel.DataAnnotations;

namespace CleanProject.Domain.Entities;

public sealed class Client : BaseEntity
{
    [StringLength(20)] public required string Username { get; init; }

    [EmailAddress] [StringLength(50)] public required string Email { get; init; }

    public Account? Account { get; init; }
    public Guid AccountId { get; set; }
}
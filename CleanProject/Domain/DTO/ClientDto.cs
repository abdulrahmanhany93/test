using System.ComponentModel.DataAnnotations;
using CleanProject.Domain.Entities;

namespace CleanProject.Domain.DTO;

public class ClientDto : BaseEntity
{
    [StringLength(20)] public required string Username { get; init; }

    [EmailAddress] [StringLength(50)] public required string Email { get; init; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanProject.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [DataType(DataType.DateTime)] public DateTime CreatedAt { get; set; }

    [DataType(DataType.DateTime)] public DateTime ModifiedAt { get; set; }
}
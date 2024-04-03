using Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class SubStation : AuditableEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Acronym { get; set; }
}

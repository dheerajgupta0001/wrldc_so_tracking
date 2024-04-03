using System.ComponentModel.DataAnnotations;
using Core.Common;

namespace Core.Entities;

public class Designation : AuditableEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public int Weight { get; set; }
}

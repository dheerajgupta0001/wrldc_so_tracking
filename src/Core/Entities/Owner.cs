using System.ComponentModel.DataAnnotations;
using Core.Common;

namespace Core.Entities;


public class Owner : AuditableEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Acronym { get; set; }
}


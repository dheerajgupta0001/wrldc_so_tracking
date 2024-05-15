using System.ComponentModel.DataAnnotations;
using Core.Common;

namespace Core.Entities;


public class ReferenceNumber : AuditableEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public int DepartmentId { get; set; }

    public Department Department { get; set; }
}


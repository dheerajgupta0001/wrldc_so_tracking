using System;
using Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Pam : AuditableEntity
{

    public int SubStationId { get; set; }

    public SubStation SubStation { get; set; }

    public int OwnerId { get; set; }

    public Owner Owner { get; set; }

    [Required]
    public string Category { get; set; }

    public int DepartmentId { get; set; }

    public Department Department { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }

    public string Remarks { get; set; }

    public DateTime StatusDate { get; set; }
}

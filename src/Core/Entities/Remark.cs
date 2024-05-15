using System.ComponentModel.DataAnnotations;
using Core.Common;

namespace Core.Entities;


public class Remark : AuditableEntity
{
    public Pam Pam { get; set; }
    public int PamId { get; set; }
    public string Remarks { get; set; }
}


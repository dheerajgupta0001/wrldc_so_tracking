using MediatR;
using System;
using System.Collections.Generic;

namespace Application.Pams.Commands.CreatePam;

public class CreatePamCommand : IRequest<List<string>>
{
    public int SubStationId { get; set; }
    public int OwnerId { get; set; }
    public string Category { get; set; }
    public int DepartmentId { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime StatusDate { get; set; }
}

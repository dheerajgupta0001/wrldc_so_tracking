using MediatR;
using System.Collections.Generic;

namespace Application.Pams.Commands.DeletePam;

public class DeletePamCommand : IRequest<List<string>>
{
    public int Id { get; set; }
}

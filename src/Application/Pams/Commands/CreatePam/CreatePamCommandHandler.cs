using Application.Common.Interfaces;
using Core.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pams.Commands.CreatePam;

class CreatePamCommandHandler : IRequestHandler<CreatePamCommand, List<string>>
{
    private readonly IAppDbContext _context;

    public CreatePamCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> Handle(CreatePamCommand request, CancellationToken cancellationToken)
    {
        Pam pam = new()
        {
            SubStationId = request.SubStationId,
            OwnerId = request.OwnerId,
            Category = request.Category,
            DepartmentId = request.DepartmentId,
            Description = request.Description,
            Status = request.Status,
            StatusDate = request.StatusDate

        };

        _context.Pams.Add(pam);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return new List<string>();
    }
}

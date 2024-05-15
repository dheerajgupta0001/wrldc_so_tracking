using Application.Common.Interfaces;
using Application.ReferenceNumbers.Commands.CreateReferenceNumber;
using Core.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ReferenceNumbers.Commands.CreateReferenceNumber;

class CreateReferenceNumberCommandHandler : IRequestHandler<CreateReferenceNumberCommand, List<string>>
{
    private readonly IAppDbContext _context;

    public CreateReferenceNumberCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<string>> Handle(CreateReferenceNumberCommand request, CancellationToken cancellationToken)
    {
        ReferenceNumber referenceNumber = new()
        {
            Name = request.Name,
            DepartmentId = request.DepartmentId

        };

        _context.ReferenceNumbers.Add(referenceNumber);
        _ = await _context.SaveChangesAsync(cancellationToken);

        return new List<string>();
    }
}

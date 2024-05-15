using Application.Common.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pams.Queries.GetPamById;

public class GetPamByIdQuery : IRequest<Pam>
{
    public int Id { get; set; }
}

public class GetPamByIdQueryHandler : IRequestHandler<GetPamByIdQuery, Pam>
{
    private readonly IAppDbContext _context;

    public GetPamByIdQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    async Task<Pam> IRequestHandler<GetPamByIdQuery, Pam>.Handle(GetPamByIdQuery request, CancellationToken cancellationToken)
    {
        Pam res = await _context.Pams.Where(co => co.Id == request.Id)
                                        .Include(n => n.Owner)
                                        .Include(n => n.SubStation)
                                        .Include(n => n.Department)
                                        .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        return res;
    }
}

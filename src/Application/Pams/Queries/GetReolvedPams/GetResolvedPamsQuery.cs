using Application.Common.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pams.Queries.GetResolvedPams;

public class GetResolvedPamsQuery : IRequest<List<Pam>>
{
    public class GetResolvedPamsQueryHandler : IRequestHandler<GetResolvedPamsQuery, List<Pam>>
    {
        private readonly IAppDbContext _context;

        public GetResolvedPamsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pam>> Handle(GetResolvedPamsQuery request, CancellationToken cancellationToken)
        {
            List<Pam> res = await _context.Pams
                                            .Include(co => co.Owner)
                                            .Include(co => co.SubStation)
                                            .Include(co => co.Department)
                                            .ToListAsync();

            List<Pam> resolvedRes = res.Where(p => p.Status == "Resolved")
                .ToList();
            return resolvedRes;
        }
    }
}

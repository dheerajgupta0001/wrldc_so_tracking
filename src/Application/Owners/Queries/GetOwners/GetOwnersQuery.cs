using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Core.Entities;

namespace Application.Owners.Queries.GetOwners;

public class GetOwnersQuery : IRequest<List<Owner>>
{
    public class GetOwnersQueryHandler : IRequestHandler<GetOwnersQuery, List<Owner>>
    {
        private readonly IAppDbContext _context;

        public GetOwnersQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Owner>> Handle(GetOwnersQuery request, CancellationToken cancellationToken)
        {
            List<Owner> res = await _context.Owners.ToListAsync();
            return res;
        }
    }
}

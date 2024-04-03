using Application.Common.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pams.Queries.GetPams;

public class GetPamsQuery : IRequest<List<Pam>>
{
    public class GetPamsQueryHandler : IRequestHandler<GetPamsQuery, List<Pam>>
    {
        private readonly IAppDbContext _context;

        public GetPamsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pam>> Handle(GetPamsQuery request, CancellationToken cancellationToken)
        {
            List<Pam> res = await _context.Pams.ToListAsync();
            return res;
        }
    }
}

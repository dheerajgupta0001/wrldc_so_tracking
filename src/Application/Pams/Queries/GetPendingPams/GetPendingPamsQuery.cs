using Application.Common.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pams.Queries.GetPendingPams;

public class GetPendingPamsQuery : IRequest<List<Pam>>
{
    public class GetPendingPamsQueryHandler : IRequestHandler<GetPendingPamsQuery, List<Pam>>
    {
        private readonly IAppDbContext _context;

        public GetPendingPamsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pam>> Handle(GetPendingPamsQuery request, CancellationToken cancellationToken)
        {
            List<Pam> res = await _context.Pams
                                            .Include(co => co.Owner)
                                            .Include(co => co.SubStation)
                                            .Include(co => co.Department)
                                            .ToListAsync();
            // get pending pams only
            //foreach (var item in res)
            //{
             List<Pam> pendingRes = res.Where(p => p.Status != "Resolved")
                .ToList();
            //}
            return pendingRes;
        }
    }
}

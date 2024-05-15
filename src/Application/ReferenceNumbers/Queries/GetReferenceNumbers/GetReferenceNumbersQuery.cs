using Application.Common.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ReferenceNumbers.Queries.GetReferenceNumbers;

public class GetReferenceNumbersQuery : IRequest<List<ReferenceNumber>>
{
    public class GetReferenceNumbersQueryHandler : IRequestHandler<GetReferenceNumbersQuery, List<ReferenceNumber>>
    {
        private readonly IAppDbContext _context;

        public GetReferenceNumbersQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReferenceNumber>> Handle(GetReferenceNumbersQuery request, CancellationToken cancellationToken)
        {
            List<ReferenceNumber> res = await _context.ReferenceNumbers
                                            .Include(co => co.Department)
                                            .ToListAsync();
            return res;
        }
    }
}

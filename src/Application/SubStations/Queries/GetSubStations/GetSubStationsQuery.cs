using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Core.Entities;

namespace Application.SubStations.Queries.GetSubStations;

public class GetSubStationsQuery : IRequest<List<SubStation>>
{
    public class GetSubStationsQueryHandler : IRequestHandler<GetSubStationsQuery, List<SubStation>>
    {
        private readonly IAppDbContext _context;

        public GetSubStationsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubStation>> Handle(GetSubStationsQuery request, CancellationToken cancellationToken)
        {
            List<SubStation> res = await _context.SubStations.ToListAsync();
            return res;
        }
    }
}

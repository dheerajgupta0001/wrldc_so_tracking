using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Core.Entities;

namespace Application.SubStations.Commands.SubStations;

public class SeedSubStationsCommand : IRequest<bool>
{
    public class SeedSubStationsCommandHandler : IRequestHandler<SeedSubStationsCommand, bool>
    {
        private readonly IAppDbContext _context;

        public SeedSubStationsCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(SeedSubStationsCommand request, CancellationToken cancellationToken)
        {
            List<string> seedSubStations = new() { "NA" };
            foreach (var subStation in seedSubStations)
            {
                bool isSubStationPres = await _context.SubStations.AnyAsync(d => d.Name.ToLower().Equals(subStation.ToLower()), cancellationToken: cancellationToken);
                if (!isSubStationPres)
                {
                    _context.SubStations.Add(new SubStation() { Name = subStation, Acronym = subStation });
                    _ = await _context.SaveChangesAsync(cancellationToken);
                }
            }
            return true;
        }
    }
}

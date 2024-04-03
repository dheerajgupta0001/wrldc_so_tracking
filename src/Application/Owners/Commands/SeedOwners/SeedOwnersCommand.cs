using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Core.Entities;

namespace Application.Owners.Commands.SeedOwners;

public class SeedOwnersCommand : IRequest<bool>
{
    public class SeedOwnersCommandHandler : IRequestHandler<SeedOwnersCommand, bool>
    {
        private readonly IAppDbContext _context;

        public SeedOwnersCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(SeedOwnersCommand request, CancellationToken cancellationToken)
        {
            List<string> seedOwners = new() { "NA" };
            foreach (var owner in seedOwners)
            {
                bool isOwnerPres = await _context.Owners.AnyAsync(d => d.Name.ToLower().Equals(owner.ToLower()), cancellationToken: cancellationToken);
                if (!isOwnerPres)
                {
                    _context.Owners.Add(new Owner() { Name = owner, Acronym = owner });
                    _ = await _context.SaveChangesAsync(cancellationToken);
                }
            }
            return true;
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Core.Entities;

namespace Application.Designations.Commands.SeedDesignations;

public class SeedDesignationsCommand : IRequest<bool>
{
    public class SeedDesignationsCommandHandler : IRequestHandler<SeedDesignationsCommand, bool>
    {
        private readonly IAppDbContext _context;

        public SeedDesignationsCommandHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(SeedDesignationsCommand request, CancellationToken cancellationToken)
        {
            List<string> seedDesigs = new() { "NA" };
            foreach (var desig in seedDesigs)
            {
                bool isDesigPres = await _context.Designations.AnyAsync(d => d.Name.ToLower().Equals(desig.ToLower()), cancellationToken: cancellationToken);
                if (!isDesigPres)
                {
                    _context.Designations.Add(new Designation() { Name = desig, Weight = 0 });
                    _ = await _context.SaveChangesAsync(cancellationToken);
                }
            }
            return true;
        }
    }
}

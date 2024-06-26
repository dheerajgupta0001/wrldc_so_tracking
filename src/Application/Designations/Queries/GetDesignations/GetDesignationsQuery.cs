﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Core.Entities;

namespace Application.Designations.Queries.GetDesignations;

public class GetDesignationsQuery : IRequest<List<Designation>>
{
    public class GetDesignationsQueryHandler : IRequestHandler<GetDesignationsQuery, List<Designation>>
    {
        private readonly IAppDbContext _context;

        public GetDesignationsQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Designation>> Handle(GetDesignationsQuery request, CancellationToken cancellationToken)
        {
            List<Designation> res = await _context.Designations.ToListAsync();
            return res;
        }
    }
}

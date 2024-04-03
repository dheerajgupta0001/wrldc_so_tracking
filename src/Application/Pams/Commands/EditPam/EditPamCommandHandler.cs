using Application.Common.Interfaces;
using Application.Common;
using Application.Users;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Pams.Commands.EditPam;

public class EditPamCommandHandler : IRequestHandler<EditPamCommand, List<string>>
{
    private readonly ILogger<EditPamCommandHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUserService _currentUserService;
    private readonly IAppDbContext _context;

    public EditPamCommandHandler(ILogger<EditPamCommandHandler> logger, IAppDbContext context, ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _context = context;
        _currentUserService = currentUserService;
        _userManager = userManager;
    }

    public async Task<List<string>> Handle(EditPamCommand request, CancellationToken cancellationToken)
    {
        string curUsrId = _currentUserService.UserId;
        ApplicationUser curUsr = await _userManager.FindByIdAsync(curUsrId);
        if (curUsr == null)
        {
            var errorMsg = "User not found for editing proposal";
            _logger.LogError(errorMsg);
            return new List<string>() { errorMsg };
        }

        // fetch the Pam for editing
        var pam = await _context.Pams.Where(ns => ns.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

        if (pam == null)
        {
            string errorMsg = $"Proposal Id {request.Id} not present for editing";
            return new List<string>() { errorMsg };
        }

        // check if user is authorized for editing proposal
        IList<string> usrRoles = await _userManager.GetRolesAsync(curUsr);
        if (curUsr.UserName != pam.CreatedBy && !usrRoles.Contains(SecurityConstants.AdminRoleString))
        {
            return new List<string>() { "This user is not authorized for updating this proposal since this is not his created by this user and he is not in admin role" };
        }

        if (pam.SubStationId != request.SubStationId) //new field
        {
            pam.SubStationId = request.SubStationId;
        }
        if (pam.OwnerId != request.OwnerId)
        {
            pam.OwnerId = request.OwnerId;
        }
        if (pam.Category != request.Category)
        {
            pam.Category = request.Category;
        }
        if (pam.DepartmentId != request.DepartmentId)
        {
            pam.DepartmentId = request.DepartmentId;
        }
        if (pam.Description != request.Description)
        {
            pam.Description = request.Description;
        }
        if (pam.Status != request.Status)
        {
            pam.Status = request.Status;
        }
        if (pam.StatusDate != request.StatusDate)
        {
            pam.StatusDate = request.StatusDate;
        }

        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Pams.Any(e => e.Id == request.Id))
            {
                return new List<string>() { $"Order Id {request.Id} not present for editing" };
            }
            else
            {
                throw;
            }
        }

        return new List<string>();

    }
}

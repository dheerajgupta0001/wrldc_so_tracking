using FluentValidation.AspNetCore;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.Departments.Queries.GetDepartments;
using Application.Users;
using Application.Users.Commands.CreateUser;
using WebApp.Extensions;
using Application.Designations.Queries.GetDesignations;

namespace WebApp.Pages.Users;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class CreateModel : PageModel
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;

    public CreateModel(ILogger<IndexModel> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    //https://www.learnrazorpages.com/razor-pages/forms/select-lists
    public SelectList DeptOptions { get; set; }
    public SelectList DesigOptions { get; set; }
    public SelectList URoles { get; set; }

    [BindProperty]
    public CreateUserCommand NewUser { get; set; }

    public async Task OnGetAsync()
    {
        await InitSelectListItems();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await InitSelectListItems();

        ValidationResult validationCheck = new CreateUserCommandValidator().Validate(NewUser);
        validationCheck.AddToModelState(ModelState, nameof(NewUser));

        if (!ModelState.IsValid)
        {
            return Page();
        }

        IdentityResult result = await _mediator.Send(NewUser);
        if (result.Succeeded)
        {
            _logger.LogInformation($"Created new account for {NewUser.Username}");
            return RedirectToPage($"./{nameof(Index)}").WithSuccess($"Created new user {NewUser.Username}");
        }
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return Page();

    }

    public async Task InitSelectListItems()
    {
        DeptOptions = new SelectList(await _mediator.Send(new GetDepartmentsQuery()), "Id", "Name");
        DesigOptions = new SelectList(await _mediator.Send(new GetDesignationsQuery()), "Id", "Name");
        URoles = new SelectList(SecurityConstants.GetRoles());
    }
}

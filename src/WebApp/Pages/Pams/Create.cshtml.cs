using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MediatR;
using Application.Pams;
using FluentValidation.Results;
using FluentValidation.AspNetCore;
using Application.Pams.Commands.CreatePam;
using Application.Departments.Queries.GetDepartments;
using Application.Owners.Queries.GetOwners;
using Application.SubStations.Queries.GetSubStations;

namespace WebApp.Pages.Pams;

public class CreateModel : PageModel
{
    private readonly IMediator _mediator;

    public CreateModel(IMediator mediator)
    {
        _mediator = mediator;
    }
    public SelectList DeptOptions { get; set; }
    public SelectList CategoryOptions { get; set; }
    public SelectList StatusOptions { get; set; }
    public SelectList SubStationOptions { get; set; }
    public SelectList OwnerOptions { get; set; }


    [BindProperty]
    public CreatePamCommand Pam { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        await InitSelectListItems();
        return Page();
    }


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        await InitSelectListItems();
        //Pam.ProposalOptions = ProposalOptions;
        ValidationResult validationCheck = new CreatePamCommandValidator().Validate(Pam);
        validationCheck.AddToModelState(ModelState, nameof(Pam));

        if (ModelState.IsValid)
        {
            List<string> errors = await _mediator.Send(Pam);

            if (errors.Count == 0)
            {

                return RedirectToPage("./Index");
            }

            foreach (var err in errors)
            {
                ModelState.AddModelError(string.Empty, err);
            }
        }
        return Page();



    }
    public async Task InitSelectListItems()
    {
        DeptOptions = new SelectList(await _mediator.Send(new GetDepartmentsQuery()), "Id", "Name");
        SubStationOptions = new SelectList(await _mediator.Send(new GetSubStationsQuery()), "Id", "Name");
        OwnerOptions = new SelectList(await _mediator.Send(new GetOwnersQuery()), "Id", "Name");
        CategoryOptions = new SelectList(CategoryConstants.GetCategoryOptions());
        StatusOptions = new SelectList(StatusConstants.GetStatusOptions());
    }
}

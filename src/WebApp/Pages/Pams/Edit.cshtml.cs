using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Core.Entities;
using MediatR;
using Application.Pams.Commands.EditPam;
using Application.Pams.Queries.GetPamById;
using AutoMapper;
using WebApp.Extensions;
using Application.Pams;
using Application.Departments.Queries.GetDepartments;
using Application.SubStations.Queries.GetSubStations;
using Application.Owners.Queries.GetOwners;
using FluentValidation;

namespace WebApp.Pages.Pams;

public class EditModel : PageModel
{
    private readonly ILogger<EditModel> _logger;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IAppDbContext _context;

    public EditModel(ILogger<EditModel> logger,
                     IMediator mediator, IMapper mapper, IAppDbContext context)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
        _context = context;
        _context = context;
    }

    [BindProperty]
    public EditPamCommand Pam { get; set; }
    public SelectList CategoryOptions { get; set; }
    public SelectList StatusOptions { get; set; }
    public SelectList DeptOptions { get; set; }
    public SelectList SubStationOptions { get; set; }
    public SelectList OwnerOptions { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }


        Pam pam = await _mediator.Send(new GetPamByIdQuery() { Id = id.Value });

        if (pam == null)
        {
            return NotFound();
        }

        await InitSelectListItems(pam);

        Pam = _mapper.Map<EditPamCommand>(pam);

        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync()
    {
        Pam pam = await _mediator.Send(new GetPamByIdQuery() { Id = Pam.Id });

        if (pam == null)
        {
            return NotFound();
        }
        // ValidationResult validationCheck = new EditPamCommandValidator().Validate(pam);
        // validationCheck.AddToModelState(ModelState, nameof(pam));

        await InitSelectListItems(pam);

        if (!ModelState.IsValid)
        {
            return Page();
        }


        List<string> errors = await _mediator.Send(Pam);

        foreach (var error in errors)
        {
            ModelState.AddModelError(string.Empty, error);
        }

        // check if we have any errors and redirect if successful
        if (errors.Count == 0)
        {
            _logger.LogInformation("Pam edit operation successful");
            return RedirectToPage($"./{nameof(Index)}").WithSuccess("Pam Editing done");
        }

        return Page();
    }

    public async Task InitSelectListItems(Pam pam)
    {
        DeptOptions = new SelectList(await _mediator.Send(new GetDepartmentsQuery()), "Id", "Name");
        SubStationOptions = new SelectList(await _mediator.Send(new GetSubStationsQuery()), "Id", "Name");
        OwnerOptions = new SelectList(await _mediator.Send(new GetOwnersQuery()), "Id", "Name");
        CategoryOptions = new SelectList(CategoryConstants.GetCategoryOptions());
        StatusOptions = new SelectList(StatusConstants.GetStatusOptions());
    }
}

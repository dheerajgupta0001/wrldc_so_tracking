using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Entities;
using MediatR;
using Application.Pams.Commands.DeletePam;
using WebApp.Extensions;
using Application.Pams.Queries.GetPamById;

namespace WebApp.Pages.Pams;

public class DeleteModel : PageModel
{
    private readonly IMediator _mediator;
    private readonly ILogger<EditModel> _logger;

    public DeleteModel(IMediator mediator, ILogger<EditModel> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [BindProperty]
    public Pam Pam { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Pam = await _mediator.Send(new GetPamByIdQuery() { Id = id.Value });

        if (Pam == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        List<string> errors = await _mediator.Send(new DeletePamCommand() { Id = id.Value });

        // check if we have any errors and redirect if successful
        if (errors.Count == 0)
        {
            _logger.LogInformation("Pam Entry delete operation successful");
            return RedirectToPage($"./{nameof(Index)}").WithSuccess("Pam Entry Deletion done");
        }

        return RedirectToPage($"./{nameof(Index)}").WithDanger("Unable to delete Pam...");
    }
}

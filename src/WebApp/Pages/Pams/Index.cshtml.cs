using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Entities;
using MediatR;
using Application.Pams.Queries.GetPams;

namespace WebApp.Pages.Pams;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IMediator _mediator;

    public IndexModel(ILogger<IndexModel> logger,IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public IList<Pam> Pam { get;set; }

    public async Task OnGetAsync()
    {
        Pam = (await _mediator.Send(new GetPamsQuery()));
    }
}

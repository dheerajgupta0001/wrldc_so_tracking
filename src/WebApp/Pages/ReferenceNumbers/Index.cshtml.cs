using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Entities;
using MediatR;
using Application.ReferenceNumbers.Queries.GetReferenceNumbers;

namespace WebApp.Pages.ReferenceNumbers;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IMediator _mediator;

    public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public IList<ReferenceNumber> ReferenceNumber { get; set; }

    public async Task OnGetAsync()
    {
        ReferenceNumber = (await _mediator.Send(new GetReferenceNumbersQuery()));
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Application.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using Application.Departments.Queries.GetDepartments;
using MediatR;

namespace WebApp.Pages.ReferenceNumbers;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class CreateModel : PageModel
{
    private readonly Infra.Persistence.AppDbContext _context;

    private readonly IMediator _mediator;
    public CreateModel(Infra.Persistence.AppDbContext context, IMediator mediator)
    {
        _mediator = mediator;
        _context = context;
    }
    public SelectList DeptOptions { get; set; }

    public async Task<IActionResult> OnGet()
    {
        await InitSelectListItems();
        return Page();
    }

    [BindProperty]
    public ReferenceNumber ReferenceNumber { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.ReferenceNumbers.Add(ReferenceNumber);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
    public async Task InitSelectListItems()
    {
        DeptOptions = new SelectList(await _mediator.Send(new GetDepartmentsQuery()), "Id", "Name");
    }
}

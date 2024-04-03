using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Application.Users;

namespace WebApp.Pages.SubStations;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class CreateModel : PageModel
{
    private readonly Infra.Persistence.AppDbContext _context;

    public CreateModel(Infra.Persistence.AppDbContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public SubStation SubStation { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.SubStations.Add(SubStation);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}

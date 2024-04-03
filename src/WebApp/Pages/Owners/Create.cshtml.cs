using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Application.Users;

namespace WebApp.Pages.Owners;

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
    public Owner Owner { get; set; }

    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Owners.Add(Owner);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}

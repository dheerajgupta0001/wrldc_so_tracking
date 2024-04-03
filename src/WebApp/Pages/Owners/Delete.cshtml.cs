using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Application.Users;

namespace WebApp.Pages.Owners;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class DeleteModel : PageModel
{
    private readonly Infra.Persistence.AppDbContext _context;

    public DeleteModel(Infra.Persistence.AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Owner Owner { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Owner = await _context.Owners.FirstOrDefaultAsync(m => m.Id == id);

        if (Owner == null)
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

        Owner = await _context.Owners.FindAsync(id);

        if (Owner != null)
        {
            _context.Owners.Remove(Owner);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}

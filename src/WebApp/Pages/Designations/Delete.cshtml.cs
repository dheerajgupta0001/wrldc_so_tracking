using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Application.Users;

namespace WebApp.Pages.Designations;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class DeleteModel : PageModel
{
    private readonly Infra.Persistence.AppDbContext _context;

    public DeleteModel(Infra.Persistence.AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Designation Designation { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Designation = await _context.Designations.FirstOrDefaultAsync(m => m.Id == id);

        if (Designation == null)
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

        Designation = await _context.Designations.FindAsync(id);

        if (Designation != null)
        {
            _context.Designations.Remove(Designation);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}

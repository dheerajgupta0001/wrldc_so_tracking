using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Application.Users;

namespace WebApp.Pages.SubStations;

[Authorize(Roles = SecurityConstants.AdminRoleString)]
public class DeleteModel : PageModel
{
    private readonly Infra.Persistence.AppDbContext _context;

    public DeleteModel(Infra.Persistence.AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public SubStation SubStation { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        SubStation = await _context.SubStations.FirstOrDefaultAsync(m => m.Id == id);

        if (SubStation == null)
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

        SubStation = await _context.SubStations.FindAsync(id);

        if (SubStation != null)
        {
            _context.SubStations.Remove(SubStation);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage("./Index");
    }
}

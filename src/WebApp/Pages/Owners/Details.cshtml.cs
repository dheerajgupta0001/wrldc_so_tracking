using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace WebApp.Pages.Owners;

public class DetailsModel : PageModel
{
    private readonly Infra.Persistence.AppDbContext _context;

    public DetailsModel(Infra.Persistence.AppDbContext context)
    {
        _context = context;
    }

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
}

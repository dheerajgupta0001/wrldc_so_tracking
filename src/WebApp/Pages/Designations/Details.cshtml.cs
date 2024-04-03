using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace WebApp.Pages.Designations;

public class DetailsModel : PageModel
{
    private readonly Infra.Persistence.AppDbContext _context;

    public DetailsModel(Infra.Persistence.AppDbContext context)
    {
        _context = context;
    }

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
}

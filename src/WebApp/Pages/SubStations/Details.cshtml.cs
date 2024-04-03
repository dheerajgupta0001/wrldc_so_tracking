using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace WebApp.Pages.SubStations;

public class DetailsModel : PageModel
{
    private readonly Infra.Persistence.AppDbContext _context;

    public DetailsModel(Infra.Persistence.AppDbContext context)
    {
        _context = context;
    }

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
}

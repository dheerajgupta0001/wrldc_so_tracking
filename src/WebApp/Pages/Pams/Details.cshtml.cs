using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace WebApp.Pages.Pams;

public class DetailsModel : PageModel
{
    private readonly Infra.Persistence.AppDbContext _context;

    public DetailsModel(Infra.Persistence.AppDbContext context)
    {
        _context = context;
    }
    public Pam Pam { get; set; }
    public string Date { get; set; }
    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        // create child entities for each proposal options

        Pam = await _context.Pams.Where(m => m.Id == id)
                                        .Include(n => n.Owner)
                                        .Include(n => n.SubStation)
                                        .Include(n => n.Department)
                                    .FirstOrDefaultAsync();
        
        Date = Pam.Created.ToString("dd-MM-yyyy");

        if (Pam == null)
        {
            return NotFound();
        }
        return Page();
    }
}

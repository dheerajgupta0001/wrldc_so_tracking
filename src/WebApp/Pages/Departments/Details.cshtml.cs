using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace WebApp.Pages.Departments;

public class DetailsModel : PageModel
{
    private readonly Infra.Persistence.AppDbContext _context;

    public DetailsModel(Infra.Persistence.AppDbContext context)
    {
        _context = context;
    }

    public Department Department { get; set; }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        Department = await _context.Departments.FirstOrDefaultAsync(m => m.Id == id);

        if (Department == null)
        {
            return NotFound();
        }
        return Page();
    }
}

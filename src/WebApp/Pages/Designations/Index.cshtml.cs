using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace WebApp.Pages.Designations;

public class IndexModel : PageModel
{
    private readonly Infra.Persistence.AppDbContext _context;

    public IndexModel(Infra.Persistence.AppDbContext context)
    {
        _context = context;
    }

    public IList<Designation> Designation { get;set; }

    public async Task OnGetAsync()
    {
        Designation = await _context.Designations.ToListAsync();
    }
}

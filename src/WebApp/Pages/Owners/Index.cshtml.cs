using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace WebApp.Pages.Owners;

public class IndexModel : PageModel
{
    private readonly Infra.Persistence.AppDbContext _context;

    public IndexModel(Infra.Persistence.AppDbContext context)
    {
        _context = context;
    }

    public IList<Owner> Owner { get;set; }

    public async Task OnGetAsync()
    {
        Owner = await _context.Owners.ToListAsync();
    }
}

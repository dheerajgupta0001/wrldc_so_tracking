using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace WebApp.Pages.SubStations;

public class IndexModel : PageModel
{
    private readonly Infra.Persistence.AppDbContext _context;

    public IndexModel(Infra.Persistence.AppDbContext context)
    {
        _context = context;
    }

    public IList<SubStation> SubStation { get;set; }

    public async Task OnGetAsync()
    {
        SubStation = await _context.SubStations.ToListAsync();
    }
}

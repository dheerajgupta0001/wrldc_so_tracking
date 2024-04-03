using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace WebApp.Pages.Departments;

public class IndexModel : PageModel
{
    private readonly Infra.Persistence.AppDbContext _context;

    public IndexModel(Infra.Persistence.AppDbContext context)
    {
        _context = context;
    }

    public IList<Department> Department { get;set; }

    public async Task OnGetAsync()
    {
        Department = await _context.Departments.ToListAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Core.Entities;

namespace Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Department> Departments { get; set; }
    DbSet<Designation> Designations { get; set; }
    DbSet<Owner> Owners { get; set; }
    DbSet<SubStation> SubStations { get; set; }
    DbSet<ReferenceNumber> ReferenceNumbers { get; set; }
    DbSet<Pam> Pams { get; set; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    EntityEntry Attach([NotNullAttribute] object entity);
}

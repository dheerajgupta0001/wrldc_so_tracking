using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Common.Interfaces;
using Core.Common;
using Core.Entities;
using System.Linq;

namespace Infra.Persistence;

public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
{
    private readonly ICurrentUserService _currentUserService;
    public DbSet<Department> Departments { get; set; }
    public DbSet<Designation> Designations { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<SubStation> SubStations { get; set; }
    public DbSet<ReferenceNumber> ReferenceNumbers { get; set; }
    public DbSet<Pam> Pams { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService)
        : base(options)
    {
        _currentUserService = currentUserService;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        foreach(var property in builder.Model.GetEntityTypes()
             .SelectMany(t => t.GetProperties())
             .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?)))
        property.SetColumnType("timestamp without time zone");
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService.UserName;
                    entry.Entity.Created = DateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _currentUserService.UserName;
                    entry.Entity.LastModified = DateTime.Now;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}

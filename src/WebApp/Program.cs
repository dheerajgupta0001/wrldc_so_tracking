using Application;
using Application.Common;
using Application.Users.Commands.SeedUsers;
using Infra;
using MediatR;
using Microsoft.AspNetCore.Mvc.Authorization;
using WebApp.Services;
using Application.Departments.Commands.SeedDepartments;
using Application.Designations.Commands.SeedDesignations;
using Application.Owners.Commands.SeedOwners;
using Application.SubStations.Commands.SubStations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);
//builder.Services.AddControllers();
builder.Services.AddRazorPages()
    .AddMvcOptions(o => o.Filters.Add(new AuthorizeFilter()))
    .AddRazorRuntimeCompilation();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

SeedData(app).Wait();

app.Run();

static async Task SeedData(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
    _ = await mediator.Send(new SeedDepartmentsCommand());
    _ = await mediator.Send(new SeedDesignationsCommand());
    _ = await mediator.Send(new SeedUsersCommand());
    _ = await mediator.Send(new SeedOwnersCommand());
    _ = await mediator.Send(new SeedSubStationsCommand());
}
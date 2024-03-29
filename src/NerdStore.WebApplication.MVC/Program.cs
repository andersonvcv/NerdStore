using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Application.AutoMapper;
using NerdStore.Catalog.Data;
using NerdStore.Payment.Data;
using NerdStore.Sales.Data;
using NerdStore.WebApplication.MVC.Data;
using NerdStore.WebApplication.MVC.Setup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<CatalogContext>(options =>
{
    options.LogTo(Console.WriteLine).UseSqlServer(connectionString);
});

builder.Services.AddDbContext<SalesContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<PaymentContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(typeof(DomainToDTOMappingProfile), typeof(DTOToDomainMappingProfile));

builder.Services.AddMediatR(typeof(Program));

builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Display}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

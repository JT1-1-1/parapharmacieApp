using Microsoft.EntityFrameworkCore;
using ParapharmacieApp.Models;
using Microsoft.AspNetCore.Identity;
using ParapharmacieApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ParapharmacieDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("gestionParapharmacieContextConnection")));
builder.Services.AddDbContext<ParapharmacieAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("gestionParapharmacieContextConnection")));
builder.Services.AddRazorPages();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ParapharmacieAppContext>();
var app = builder.Build();
app.MapRazorPages();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

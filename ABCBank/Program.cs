using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ABCBank.Areas.Identity.Data;
using ABCBank.Services;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ABCBankContextConnection") ?? throw new InvalidOperationException("Connection string 'ABCBankContextConnection' not found.");

builder.Services.AddDbContext<ABCBankContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ABCBankUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ABCBankContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<ExchangeRateService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapRazorPages();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

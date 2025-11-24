using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using BonDeCollecte.Data; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

builder.Services.AddMvc();

//builder.Services.AddIdentity<Users, IdentityRole>(options =>
//{
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequiredLength = 8;
//    options.Password.RequireUppercase = false;
//    options.Password.RequireLowercase = false;
//    options.User.RequireUniqueEmail = true;
//    options.SignIn.RequireConfirmedAccount = false;
//    options.SignIn.RequireConfirmedEmail = false;
//    options.SignIn.RequireConfirmedPhoneNumber = false;
//})
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

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
// add localization languages / Globalization 
 app.Use(async (context, next) =>
{
    string? cookie = string.Empty;
if (context.Request.Cookies.TryGetValue("Language", out cookie))
{
    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cookie);
    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cookie);
}
else
{
    Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en");
    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
}
await next.Invoke();

});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

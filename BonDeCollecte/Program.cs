using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using BonDeCollecte.Data;
using BonDeCollecte.GenereToken.Services;
using BonDeCollecte.GenereToken;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Code added for CORS
const string MyAllowAnyOrigins = "_myAllowAnyOrigins";
// Add services to the container.
builder.Services.AddCors(options => {
    options.AddPolicy(
    name: MyAllowAnyOrigins,
    policy => {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();

// Configuration JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? "votre_cle_secrete_tres_longue_et_complexe";
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "votre_issuer";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "votre_audience";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization();



builder.Services.AddScoped<ITokenService, TokenService>();

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
// Code added for CORS
app.UseCors(MyAllowAnyOrigins);
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using VC.Wallet.Core;
using VC.Wallet.Data;
using VC.Wallet.WebApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMicrosoftIdentityWebAppAuthentication(builder.Configuration);
builder.Services.AddSingleton<IAuthorizationHandler, AdminAuthHandler>();
builder.Services.AddAuthorization(options => options.AddPolicy("RequireAdmin", policy => policy.Requirements.Add(new AdminAuthRequirement())));


builder.Services.AddDbContext<VCWalletDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
builder.Services.AddVCWebWalletServices(options => builder.Configuration.Bind("Configuration:AdminUsername"));

builder.Services.AddRazorPages()
.AddMicrosoftIdentityUI();

var app = builder.Build();

AdminService.config = app.Configuration;

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
using Webapp.Models;
using Webapp.APIs;
using Webapp.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Webapp;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<UsersAPI>();
        builder.Services.AddScoped<AuthenticationAPI>();

        builder.Services.AddTransient<IUserStore<Account>, AccountRepository>();
        builder.Services.AddTransient<IRoleStore<AccountRole>, AccountRoleRepository>();
        builder.Services.AddTransient<IAccountManager, AccountManager>();

        builder.Services.AddIdentity<Account, AccountRole>()
                        .AddDefaultTokenProviders();

        builder.Services.ConfigureApplicationCookie(config =>
        {
            config.LoginPath = "/Login/New";
            config.AccessDeniedPath = "/Login/AccessDenied";
            config.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            config.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        });

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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
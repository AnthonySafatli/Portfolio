using Portfolio.Data;
using Portfolio.Models;
using Portfolio.Pages.Admin;

namespace Portfolio;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
        {
            options.Conventions.AddPageRoute("/Admin/Dashboard", "/Admin/Index");
            options.Conventions.AddPageRoute("/Admin/Dashboard", "/Admin");
        });
        builder.Services.AddAuthentication(Security.Config.AdminCookieName).AddCookie(Security.Config.AdminCookieName, options =>
        {
            options.Cookie.Name = Security.Config.FromAddressPassword;
            options.LoginPath = "/Admin/Login";
            options.AccessDeniedPath = "/Admin/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
        });
        builder.Services.AddDbContext<ProjectsContext>();

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

        app.Run();
    }
}

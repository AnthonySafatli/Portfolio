using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;
using Portfolio.Pages.Admin;
using Portfolio.Services;

namespace Portfolio;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("ProjectsConnection");
        builder.Services.AddDbContext<ProjectsContext>(options =>
            options.UseSqlite(connectionString));

        builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
        {
            options.Conventions.AddPageRoute("/Admin/Dashboard", "/Admin/Index");
            options.Conventions.AddPageRoute("/Admin/Dashboard", "/Admin");
        });
        builder.Services.AddAuthentication(SecurityService.Config.AdminCookieName).AddCookie(SecurityService.Config.AdminCookieName, options =>
        {
            options.Cookie.Name = SecurityService.Config.FromAddressPassword;
            options.LoginPath = "/Admin/Login";
            options.AccessDeniedPath = "/Admin/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
        });
        builder.Services.AddSingleton<EmailService>(); 
        builder.Services.AddSingleton<PageRenderingService>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<ProjectsContext>();
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while applying migrations.");
            }
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}

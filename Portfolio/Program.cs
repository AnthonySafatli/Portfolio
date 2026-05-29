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
        builder.Services.AddDbContext<PortfolioDbContext>(options =>
            options.UseSqlite(connectionString));

        builder.Services.AddRazorPages().AddRazorPagesOptions(o =>
        {
            o.Conventions.AddPageRoute("/Admin/Dashboard", "/Admin/Index");
            o.Conventions.AddPageRoute("/Admin/Dashboard", "/Admin");
        }).AddRazorOptions(o =>
        {
            o.PageViewLocationFormats.Add("/Pages/Layouts/{0}.cshtml");
        });

        builder.Services
        .AddAuthentication(builder.Configuration["AdminCookieName"]!)
        .AddCookie(builder.Configuration["AdminCookieName"]!, options =>
        {
            options.Cookie.Name = builder.Configuration["AdminCookieName"];
            options.LoginPath = "/Admin/Login";
            options.AccessDeniedPath = "/Admin/AccessDenied";
            options.ExpireTimeSpan = TimeSpan.FromDays(30);
        });

        builder.Services.AddSingleton<LocationService>(); 

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<PortfolioDbContext>();
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

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error/500");
            app.UseHsts();
        }

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseStatusCodePagesWithReExecute("/Error/{0}");

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();

    }
}

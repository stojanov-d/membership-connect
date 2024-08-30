using BE_membership_connect.Database;
using Microsoft.EntityFrameworkCore;

public static class DatabaseExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var environment = services.GetRequiredService<IWebHostEnvironment>();

        if (environment.IsDevelopment())
        {
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
        else
        {
            var context = services.GetRequiredService<StagingDbContext>();
            context.Database.Migrate();
        }
    }
}

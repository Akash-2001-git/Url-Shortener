using Microsoft.EntityFrameworkCore;
using Url_Shortener.Database;

namespace Url_Shortener.Extensions
{
    public static class MigrationExtension
    {
        public static void Migrate(this WebApplication application)
        {
            var scope = application.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
        }
    }
}

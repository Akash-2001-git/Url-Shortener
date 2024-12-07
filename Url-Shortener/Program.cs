using Microsoft.EntityFrameworkCore;
using Url_Shortener.Database;
using Url_Shortener.Extensions;
using Url_Shortener.Job;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:UrlShortenerDb");
builder.Services.AddDbContext<AppDbContext>(db => db.UseSqlServer(connectionString));
builder.Services.AddHostedService<ExpiredUrlCleanupService>();
MediatrExtensions.RegisterMediatr(builder.Services); //register mediatr
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    MigrationExtension.Migrate(app); //migrate db
}

app.MapControllers();
try
{
    app.Run();
    return 0;
}
catch (Exception e)
{
    return 1;
}


using AspNetCoreRateLimit;
using KeyNekretnine.Configuration;
using KeyNekretnine.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCaching()
    .AddApplication()
    .AddPresentation()
    .AddCustomAttributes()
    .AddDatabase()
    .AddAuthorizationAndAuthentication()
    .AddRateLimiting()
    .AddBackgroundTasks()
    .AddManagers()
    .AddHttpConfiguration()
    .AddMapings()
    .AddServicesRegistration();


var app = builder.Build();


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseCors("Dev");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DisplayRequestDuration();
});

app.UseHttpsRedirection();

app.UseCustomExceptionHandler();

app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

using KeyNekretnine.Api.Extensions;
using KeyNekretnine.Application;
using KeyNekretnine.Infrastructure;

//builder.Services
//    .AddCaching()
//    .AddPresentation()
//    .AddCustomAttributes()
//    .AddDatabase()
//    .AddAuthorizationAndAuthentication()
//    .AddRateLimiting()
//    .AddBackgroundTasks()
//    .AddManagers()
//    .AddHttpConfiguration()
//    .AddServicesRegistration();


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

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

//app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

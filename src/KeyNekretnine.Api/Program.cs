using KeyNekretnine.Api.Extensions;
using KeyNekretnine.Application;
using KeyNekretnine.Configuration;
using KeyNekretnine.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApi();

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseCors("Dev");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.DisplayRequestDuration();
});

app.UseCustomExceptionHandler();

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

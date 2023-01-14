using Application.Behaviors;
using CompanyEmployees.Extensions;
using Contracts;
using FluentValidation;
using KeyNekretnine.Extensions;
using MediatR;
using NLog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
"/nlog.config"));

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters()
  .AddApplicationPart(typeof(KeyNekretnine.Presentation.AssemblyReference).Assembly);

builder.Services.AddMediatR(typeof(Application.AssemblyReference).Assembly);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT();
builder.Services.AddAuthentication();
builder.Services.ConfigureSqlContext();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

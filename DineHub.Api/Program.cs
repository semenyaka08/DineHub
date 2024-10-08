using DineHub.Api.Extensions;
using DineHub.Api.Middlewares;
using DineHub.Application.Extensions;
using DineHub.Domain.Entities;
using DineHub.Infrastructure;
using DineHub.Infrastructure.Seeders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentation();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

//Seeding initial data
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IDataSeeder>();
await seeder.Seed();

//Configure the Http request pipeline
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<User>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
using BuberDinner.Api;
using BuberDinner.Api.Common.Errors;
using BuberDinner.Application;
using BuberDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);


builder.Services
    .AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
var app = builder.Build();



//app.UseExceptionHandler("/error");
app.MapControllers();
app.Run();



using InsuranceWebApi.Application.AppServices;
using InsuranceWebApi.Application.AppServices.Interfaces;
using InsuranceWebApi.Domain.Entities;
using InsuranceWebApi.Domain.Repositories;
using InsuranceWebApi.Infra.Contexts;
using InsuranceWebApi.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton(new CacheContext<Advisor>(builder.Configuration.GetValue<int>("Cache:Capacity", 5)))
                .AddTransient<IAdvisorAppService, AdvisorAppService>()
                .AddTransient<IAdvisorRepository, AdvisorRepository>();

builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) 
        .AllowCredentials()); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }
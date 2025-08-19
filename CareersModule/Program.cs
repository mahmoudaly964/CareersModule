using Application.Mapping;
using Application.Services;
using Application.Services_Interfaces; // Ensure AutoMapper namespace is included
using Application.UseCases.VacancyUseCases;
using Application.UseCasesInterfaces.Vacancy;
using Application.UseCasesInterfaces.VacancyUseCase;
using AutoMapper;
using Domain.Interfaces;
using DotNetEnv;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("No database connection string found.");
}

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
//register unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Register Repositories
builder.Services.AddScoped<IVacancyRepository, VacancyRepository>();
// Register Auto mapper 
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<VacancyMapping>();
});
// Register Use Cases
builder.Services.AddScoped<IAddVacancyUseCase, AddVacancyUseCase>();
builder.Services.AddScoped<IGetVacancyUseCase, GetVacancyUseCase>();
builder.Services.AddScoped<IUpdateVacancyUseCase, UpdateVacancyUseCase>();
builder.Services.AddScoped<IDeleteVacancyUseCase, DeleteVacancyUseCase>();
builder.Services.AddScoped<IListVacancyUseCase, ListVacancyUseCase>();
builder.Services.AddScoped<IPublishVacancyUseCase, PublishVacancyUseCase>();
builder.Services.AddScoped<IUnpublishVacancyUseCase, UnpublishVacancyUseCase>();
builder.Services.AddScoped<IListPublishedVacancyUseCase, ListPublishedVacancyUseCase>();
//register services
builder.Services.AddScoped<IVacancyService, VacancyService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

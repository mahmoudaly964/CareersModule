using Application.Mapping;
using Application.Services;
using Application.Services_Interfaces; // Ensure AutoMapper namespace is included
using Application.UseCases.ApplicationUseCases;
using Application.UseCases.UserUseCases;
using Application.UseCases.VacancyUseCases;
using Application.UseCasesInterfaces.ApplicationUseCase;
using Application.UseCasesInterfaces.UserUseCase;
using Application.UseCasesInterfaces.Vacancy;
using Application.UseCasesInterfaces.VacancyUseCase;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using DotNetEnv;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Presentation.Middlewares;
using System.Text;

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
builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;

    // User settings
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")
                ?? throw new InvalidOperationException("secret environment variable is not configured");
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER")
    ?? throw new InvalidOperationException("ISSUER environment variable is not configured");
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")
    ?? throw new InvalidOperationException("AUDIENCE environment variable is not configured");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSecret)),
        ValidateIssuer = true,
        ValidIssuer = jwtIssuer,
        ValidateAudience = true,
        ValidAudience = jwtAudience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

//register unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Register Repositories
builder.Services.AddScoped<IVacancyRepository, VacancyRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
// Register Auto mapper 
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<VacancyMapping>();
    cfg.AddProfile<ApplicationMapping>();
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
builder.Services.AddScoped<IGetApplicationUseCase, GetApplicationUseCase>();
builder.Services.AddScoped<IAddApplicationUseCase, AddApplicationUseCase>();
builder.Services.AddScoped<IListApplicationsUseCase, ListApplicationsUseCase>();
builder.Services.AddScoped<IUpdateApplicationStatusUseCase, UpdateApplicationStatusUseCase>();

builder.Services.AddScoped<ISignUpUseCase, SignUpUseCase>();
builder.Services.AddScoped<ILogInUseCase, LogInUseCase>();
//register services
builder.Services.AddScoped<IVacancyService, VacancyService>();
builder.Services.AddScoped<IApplicationService,ApplicationService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CareersModule ", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

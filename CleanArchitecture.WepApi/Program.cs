using CleanArchitecture.Application.Behaviors;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Repositories;
using CleanArchitecture.Persistance.Services;
using CleanArchitecture.WepApi.Middleware;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<AppDbContext>());
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddAutoMapper(typeof(CleanArchitecture.Persistance.AssemblyRefence).Assembly);


string connectionString = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireUppercase = false;
}
    
    ).AddEntityFrameworkStores<AppDbContext>();
  

builder.Services.AddControllers()
    .AddApplicationPart(typeof(CleanArchitecture.Presentation.AssemblyReferance).Assembly);

builder.Services.AddMediatR(cfr => 
    cfr.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyRefence).Assembly));

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssembly(typeof(CleanArchitecture.Application.AssemblyRefence).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorMiddlewareExtansions();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

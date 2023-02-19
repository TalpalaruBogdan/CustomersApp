using FluentValidation;
using Microsoft.EntityFrameworkCore;
using CustomerService.Data;
using CustomerService.Endpoints;
using CustomerService.Models;
using CustomerService.Repository;
using CustomerService.Validator;
using CustomerService.DTOs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>(); 
builder.Services.AddSingleton<IValidator<UpdateCustomerDto>, UpdateCustomerDtoValidator>();
builder.Services.AddSingleton<IValidator<Customer>, CustomerValidator>();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddEndpoints();

app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:4200");
});

app.Run();
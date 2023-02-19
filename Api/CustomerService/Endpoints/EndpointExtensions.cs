using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using CustomerService.Models;
using CustomerService.Repository;
using CustomerService.DTOs;

namespace CustomerService.Endpoints
{
    public static class EndpointExtensions
    {
        public static void AddEndpoints(this WebApplication app)
        {
            app.MapGet("/api/customers", (ICustomerRepository repo) =>
            {
                return Results.Ok(repo.GetCustomers());
            })
                .WithName("GetCustomers")
                .WithOpenApi()
                .Produces(200);

            app.MapGet("/api/customer/{id}", async (Guid id, ICustomerRepository repo) =>
            {
                var found = await repo.GetCustomer(id);
                return found is null ?
                    Results.NotFound() :
                    Results.Ok(found);
            })
                .WithName("GetCustomer")
                .WithOpenApi()
                .Produces(200)
                .Produces(404);

            app.MapDelete("/api/customer/{id}", async (Guid id, ICustomerRepository repo) =>
            {
                var found = await repo.GetCustomer(id);
                if (found is null)
                {
                    return Results.NotFound();
                }
                await repo.DeleteCustomer(found);
                return Results.NoContent();
            })
                .WithName("DeleteCustomer")
                .WithOpenApi()
                .Produces(200)
                .Produces(404);

            app.MapPut("api/customer/{id}", async (Guid id, UpdateCustomerDto customer, ICustomerRepository repo, IValidator<UpdateCustomerDto> validator) =>
            {
                var idSearch = await repo.GetCustomer(id);
                var emailSearch = await repo.GetCustomer(customer.Email);
                var validationResult = validator.Validate(customer);
                if (customer is not UpdateCustomerDto dto || !validationResult.IsValid)
                {
                    return Results.UnprocessableEntity(customer);
                }
                if (idSearch is null)
                {
                    return Results.NotFound(idSearch);
                }
                if (emailSearch is not null)
                {
                    return Results.Conflict(customer.Email);
                }
                await repo.UpdateCustomer(id, customer);
                return Results.Ok(customer);
            })
                .WithName("UpdateCustomer")
                .Produces(404)
                .Produces(409)
                .Produces(422)
                .Produces(200)
                .WithOpenApi();

            app.MapPost("/api/customer", async ([FromBody]Customer customer, ICustomerRepository repo, IValidator<Customer> validator) =>
            {
                var validationResult = await validator.ValidateAsync(customer);
                if (!validationResult.IsValid)
                {
                    return Results.UnprocessableEntity(validationResult.Errors);
                }
                var searchById = await repo.GetCustomer(customer.Id);
                var searchByEmail = await repo.GetCustomer(customer.Email);
                if (searchById is not null || searchByEmail is not null)
                {
                    return Results.Conflict();
                }
                await repo.AddCustomer(customer);
                return Results.Created($"/api/customer/{customer.Id}", customer);

            })
                .WithName("CreateCustomer")
                .WithOpenApi()
                .Produces(201)
                .Produces(409)
                .Produces(422);
        }
    }
}

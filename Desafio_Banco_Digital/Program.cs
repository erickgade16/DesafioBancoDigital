using Application;
using DesafioBancoDigital.Domain.Interface;
using DesafioBancoDigital.Infrastructure.Context;
using DesafioBancoDigital.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using DesafioBancoDigital.API.GraphQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder =>
        {
            builder.WithOrigins("http://localhost:5180") // Permita o seu frontend (porta atualizada)
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<QueryQL>()
    .AddMutationType<Mutation>()
    .AddFiltering()
    .AddSorting();

builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<ContaServices>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowFrontend"); // Use a política de CORS

app.MapGraphQL();

app.Run();

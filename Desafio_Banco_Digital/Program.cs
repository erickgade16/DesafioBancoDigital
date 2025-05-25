using Application;
using DesafioBancoDigital.Domain.Interface;
using DesafioBancoDigital.Infrastructure.Context;
using DesafioBancoDigital.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using DesafioBancoDigital.API.GraphQL;

var builder = WebApplication.CreateBuilder(args);

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

app.MapGraphQL();

app.Run();

using Microsoft.EntityFrameworkCore;
using Person.Application.Interfaces;
using Person.Infrastructure.Data;
using Person.Infrastructure.Repositories; 
using Person.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();

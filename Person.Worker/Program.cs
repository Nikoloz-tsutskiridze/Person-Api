using Microsoft.EntityFrameworkCore;
using Person.Application.Interfaces;
using Person.Infrastructure.Data;
using Person.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();

var host = builder.Build();
host.Run();

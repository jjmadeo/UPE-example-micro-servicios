using Microsoft.EntityFrameworkCore;
using ms_cards.Models;
using ms_cards.Services;
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Configuration.AddConfiguration(configuration);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(10, 5))));

builder.Services.AddHostedService<ConsumerService>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();
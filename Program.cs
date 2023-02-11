using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PracticeFullstackApp.Contexts;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using FluentAssertions.Common;
using PracticeFullstackApp.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// custom code for database connection
var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

builder.Services.AddDbContext<PracticeDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<Utility>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS 

var corsSettings = configuration.GetSection("CorsSettings");
var allowedOrigin = corsSettings.GetValue<string>("AllowedOrigin");

app.UseCors(policy => policy.AllowAnyOrigin());

//app.UseCors(policy => policy.WithOrigins(allowedOrigin)
//                                        .AllowAnyHeader()
//                                        .AllowAnyMethod()
//                                        .AllowCredentials()
//                                        );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// another test
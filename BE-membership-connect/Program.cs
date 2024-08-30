using BE_membership_connect.Database;
using BE_membership_connect.Models;
using dotenv.net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

var environment = builder.Environment.EnvironmentName;
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
  .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
  .AddEnvironmentVariables();


var host = Environment.GetEnvironmentVariable("DB_HOST");
var port = Environment.GetEnvironmentVariable("DB_PORT");
var database = Environment.GetEnvironmentVariable("DB_NAME");
var user = Environment.GetEnvironmentVariable("DB_USER");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

var connectionString = $"Host={host};Port={port};Database={database};Username={user};Password={password}";

// This is the connection for the local database
if (builder.Environment.IsDevelopment())
{
  builder.Services.AddDbContext<AppDbContext>(options =>
      options.UseNpgsql(connectionString));
}

// This is the connection for the docker container
if (builder.Environment.IsStaging())
{
  builder.Services.AddDbContext<AppDbContext>(options =>
      options.UseNpgsql("DefaultConnection"));
}

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
  options.User.RequireUniqueEmail = true;
  options.Password.RequireDigit = true;
  options.Password.RequireLowercase = true;
  options.Password.RequireUppercase = true;
  options.Password.RequireNonAlphanumeric = true;
  options.Password.RequiredLength = 8;
})
  .AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

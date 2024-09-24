using BE_membership_connect.Database;
using BE_membership_connect.Models;
using BE_membership_connect.Repository;
using BE_membership_connect.Repository.Interfaces;
using BE_membership_connect.Services;
using BE_membership_connect.Services.Interfaces;
using dotenv.net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

var environment = builder.Environment.EnvironmentName;
var identityEnvironment = builder.Environment;
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
  .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
  .AddEnvironmentVariables();


var host = Environment.GetEnvironmentVariable("DB_HOST");
var port = Environment.GetEnvironmentVariable("DB_PORT");
var database = Environment.GetEnvironmentVariable("DB_NAME");
var user = Environment.GetEnvironmentVariable("DB_USER");
var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

Console.WriteLine($"Testing CI/CD with GitHub Actions.");

var connectionString = $"Host={host};Port={port};Database={database};Username={user};Password={password}";
Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");


builder.Services.AddScoped<IMembershipService, MembershipService>();
builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();

// This is the connection for the local database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));


// This is the connection for the docker container
builder.Services.AddDbContext<StagingDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.LoadIdentityByEnvironment(identityEnvironment);

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowFrontend",
      builder =>
      {
        builder.WithOrigins("http://localhost:3000", "http://membershipconnect") // Replace with your frontend URL
                 .AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowCredentials();
      });
});



// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = IdentityConstants.BearerScheme;
  options.DefaultChallengeScheme = IdentityConstants.BearerScheme;
})
    .AddBearerToken(IdentityConstants.BearerScheme)
    .AddCookie(IdentityConstants.ApplicationScheme);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "Membership Connect API", Version = "v1" });

  // Define the Bearer scheme used for authentication
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "Enter your token in the text input below. Example: '12345abcdef'."
  });

  c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
var app = builder.Build();

app.ApplyMigrations();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapIdentityApi<AppUser>();

app.MapControllers();

app.Run();
